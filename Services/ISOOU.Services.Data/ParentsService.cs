namespace ISOOU.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ParentsService : IParentsService
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IRepository<Parent> parentsRepository;
        private readonly IAddressesService addressesService;
        private readonly IDistrictsService districtsService;


        public ParentsService(
            UserManager<SystemUser> userManager,
            IRepository<Parent> parentsRepository,
            IAddressesService addressesService,
            IDistrictsService districtsService)
        {
            this.userManager = userManager;
            this.parentsRepository = parentsRepository;
            this.addressesService = addressesService;
            this.districtsService = districtsService;
        }

        public async Task<bool> Create(ClaimsPrincipal userIdentity, ParentServiceModel parentServiceModel)
        {
            if (parentServiceModel == null)
            {
                throw new ArgumentNullException();
            }

            var userId = this.userManager.GetUserId(userIdentity);

            var parent = parentServiceModel.To<Parent>();
            parent.UserId = userId;

            await this.parentsRepository.AddAsync(parent);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<ParentServiceModel> GetParents(ClaimsPrincipal userIdentity)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var parents = this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId)
                .To<ParentServiceModel>();

            return parents;
        }

        public async Task<string> GetParentFullNameByRole(ClaimsPrincipal userIdentity, ParentRole role)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var parent = await this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId)
                .Where(r => r.Role == role)
                .SingleOrDefaultAsync();

            return parent.FullName;
        }

        public int GetParentIdByFullName(ClaimsPrincipal userIdentity, string fullName)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var parent = this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId)
                .Where(r => r.FullName.Equals(fullName))
                .SingleOrDefaultAsync()
                .To<ParentServiceModel>();
            var result = 0;

            if (fullName != ParentRole.Друг.ToString() || fullName != ParentRole.Неизвестен.ToString())
            {
                result = parent.Id;
            }

            return result;
        }

        public async Task<bool> Edit(string userIdentity, ParentServiceModel parentServiceModel)
        {
            District workDistrict = (await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(a => a.Id == parentServiceModel.Id)).WorkDistrict;
            if (workDistrict == null)
            {
                throw new ArgumentNullException(nameof(workDistrict));
            }

            District permanentDistrict = (await this.districtsService
                                            .GetDistrictById(parentServiceModel.Address.PermanentDistrictId))
                                            .To<District>();

            if (permanentDistrict == null)
            {
                throw new ArgumentNullException(nameof(permanentDistrict));
            }

            District currentDistrict = (await this.districtsService
                                            .GetDistrictById(parentServiceModel.Address.CurrentDistrictId))
                                            .To<District>();
            if (currentDistrict == null)
            {
                throw new ArgumentNullException(nameof(currentDistrict));
            }

            AddressDetails addressToEdit = (await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(a => a.Id == parentServiceModel.Address.Id)).Address;

            if (addressToEdit == null)
            {
                throw new ArgumentNullException();
            }

            addressToEdit.Current = parentServiceModel.Address.Current;
            addressToEdit.Permanent = parentServiceModel.Address.Permanent;
            addressToEdit.CurrentCity = parentServiceModel.Address.CurrentCity;
            addressToEdit.PermanentCity = parentServiceModel.Address.PermanentCity;
            addressToEdit.CurrentDistrict = currentDistrict;
            addressToEdit.PermanentDistrict = permanentDistrict;
            addressToEdit.CurrentDistrictId = currentDistrict.Id;
            addressToEdit.PermanentDistrictId = permanentDistrict.Id;

            var parentToEdit = await this.parentsRepository
                               .All()
                               .FirstOrDefaultAsync(p => p.Id == parentServiceModel.Id);

            if (parentToEdit == null)
            {
                throw new ArgumentNullException();
            }

            //var user = (await this.usersService.GetUserByuserName(userIdentity)).To<SystemUser>();

            //parentToEdit.User = user;
            parentToEdit.UCN = parentToEdit.UCN;
            parentToEdit.Role = parentToEdit.Role;
            parentToEdit.WorkDistrict = workDistrict;
            parentToEdit.Address = addressToEdit;

            this.parentsRepository.Update(parentToEdit);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ParentServiceModel> GetParentById(int id)
        {
            ParentServiceModel parent = (await this.parentsRepository.All()
                .ToListAsync())
                .FirstOrDefault(p => p.Id == id)
                .To<ParentServiceModel>();

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            AddressDetailsServiceModel address = (await this.addressesService.GetAddressDetailsById(parent.AddressId))
                                                    .To<AddressDetailsServiceModel>();

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            DistrictServiceModel workDistrict = await this.districtsService.GetDistrictById(parent.WorkDistrictId);

            if (workDistrict == null)
            {
                throw new ArgumentNullException(nameof(workDistrict));
            }

            parent.WorkDistrict = workDistrict;
            parent.Address = address;


            return parent;
        }

        public async Task<bool> Delete(int id)
        {
            var parentToDelete = await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == id);

            if (parentToDelete == null)
            {
                throw new ArgumentNullException();
            }

            parentToDelete.IsDeleted = true;
            this.parentsRepository.Delete(parentToDelete);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
