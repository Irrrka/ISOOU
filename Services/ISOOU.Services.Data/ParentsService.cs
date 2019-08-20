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
        private readonly IRepository<AddressDetails> addressesRepository;
        private readonly IAddressesService addressesService;
        private readonly IDistrictsService districtsService;


        public ParentsService(
            UserManager<SystemUser> userManager,
            IRepository<Parent> parentsRepository,
            IAddressesService addressesService,
            IDistrictsService districtsService,
            IRepository<AddressDetails> addressesRepository)
        {
            this.userManager = userManager;
            this.parentsRepository = parentsRepository;
            this.addressesService = addressesService;
            this.districtsService = districtsService;
            this.addressesRepository = addressesRepository;
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

        public IQueryable<ParentServiceModel> GetParentsWithOtherAndNull(ClaimsPrincipal userIdentity)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var parents = this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId
                || u.FullName.TrimEnd() == ParentRole.Друг.ToString()
                || u.FullName.TrimEnd() == ParentRole.Няма.ToString())
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

        public async Task<int> GetParentIdByFullName(ClaimsPrincipal userIdentity, string fullName)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var parent = await this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId)
                .Where(r => r.FullName.Equals(fullName))
                .SingleOrDefaultAsync();

            var result = 0;

            if (fullName != ParentRole.Друг.ToString() || fullName != ParentRole.Няма.ToString())
            {
                result = parent.Id;
            }

            return result;
        }

        public async Task<bool> Edit(int id, ParentServiceModel parentServiceModel)
        {
            Parent parentToEdit = await this.parentsRepository.All()
                             .FirstOrDefaultAsync(p => p.Id == parentServiceModel.Id);
            if (parentToEdit == null)
            {
                throw new ArgumentNullException();
            }

            District workDistrict = (await this.parentsRepository.All()
                                                .FirstOrDefaultAsync(a => a.Id == parentServiceModel.Id)).WorkDistrict;
            if (workDistrict == null)
            {
                throw new ArgumentNullException(nameof(workDistrict));
            }

            parentToEdit.WorkDistrict = workDistrict;

            parentToEdit.Address.PermanentDistrictId = parentServiceModel.Address.PermanentDistrictId;
            parentToEdit.Address.CurrentDistrictId = parentServiceModel.Address.CurrentDistrictId;
            parentToEdit.Address.CurrentCity = parentServiceModel.Address.CurrentCity;
            parentToEdit.Address.PermanentCity = parentServiceModel.Address.PermanentCity;
            parentToEdit.Address.Current = parentServiceModel.Address.Current;
            parentToEdit.Address.Permanent = parentServiceModel.Address.Permanent;
            var addressToEdit = (await this.parentsRepository.All()
                                    .SingleOrDefaultAsync(a => a.AddressId == parentServiceModel.AddressId)).Address;

            parentToEdit.FirstName = parentServiceModel.FirstName;
            parentToEdit.MiddleName = parentServiceModel.MiddleName;
            parentToEdit.LastName = parentServiceModel.LastName;
            parentToEdit.PhoneNumber = parentServiceModel.PhoneNumber;
            parentToEdit.WorkName = parentServiceModel.WorkName;

            parentToEdit.UCN = parentToEdit.UCN;
            parentToEdit.Role = parentToEdit.Role;

            this.addressesRepository.Update(addressToEdit);
            this.parentsRepository.Update(parentToEdit);
            var result = await this.parentsRepository.SaveChangesAsync();
            result = await this.addressesRepository.SaveChangesAsync();

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
                               .Include(c => c.Candidates)
                               .SingleOrDefaultAsync(p => p.Id == id);

            if (parentToDelete == null)
            {
                throw new ArgumentNullException();
            }

            parentToDelete.IsDeleted = true;

            this.parentsRepository.Update(parentToDelete);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<string> GetParentsRoleByUser(ClaimsPrincipal userIdentity)
        {
            var userId = this.userManager.GetUserId(userIdentity);
            var exstParent = await this.parentsRepository
                .All()
                .Where(u => u.User.Id == userId)
                .SingleOrDefaultAsync();

            var role = exstParent.Role.ToString();

            return role;
        }
    }
}
