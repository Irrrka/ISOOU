namespace ISOOU.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ISOOU.Common;
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
            IDistrictsService districtsService
            )
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
                throw new ArgumentNullException(nameof(parentServiceModel));
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
            if (parent == null)
            {
                result = (await this.parentsRepository
                .All()
                .Where(r => r.FullName.TrimEnd().Equals(fullName.TrimEnd()))
                .SingleOrDefaultAsync()).Id;
            }
            else
            {
                result = parent.Id;
            }

            return result;
        }

        public async Task<bool> Edit(int id, ParentServiceModel parentServiceModel)
        {
            Parent parentToEdit = await this.parentsRepository.All()
                                            .FirstOrDefaultAsync(p => p.Id == id);
            if (parentToEdit == null)
            {
                throw new ArgumentNullException(string.Format(GlobalConstants.NullReferenceParentId, id));
            }

            parentToEdit.FirstName = parentServiceModel.FirstName;
            parentToEdit.MiddleName = parentServiceModel.MiddleName;
            parentToEdit.LastName = parentServiceModel.LastName;
            parentToEdit.PhoneNumber = parentServiceModel.PhoneNumber;
            parentToEdit.WorkName = parentServiceModel.WorkName;
            parentToEdit.WorkDistrictId = parentServiceModel.WorkDistrictId;

            parentToEdit.UCN = parentToEdit.UCN;
            parentToEdit.Role = parentToEdit.Role;

            var addressToEdit = (await this.parentsRepository.All()
                                    .SingleOrDefaultAsync(a => a.AddressId == parentServiceModel.AddressId)).Address;

            //var currDistrictId = addressToEdit.CurrentDistrictId;
            //var permDistrictId = addressToEdit.PermanentDistrictId;

            addressToEdit.PermanentDistrictId = parentServiceModel.Address.PermanentDistrictId;
            addressToEdit.CurrentDistrictId = parentServiceModel.Address.CurrentDistrictId;
            addressToEdit.CurrentCity = parentServiceModel.Address.CurrentCity;
            addressToEdit.PermanentCity = parentServiceModel.Address.PermanentCity;
            addressToEdit.Current = parentServiceModel.Address.Current;
            addressToEdit.Permanent = parentServiceModel.Address.Permanent;

            await this.addressesService.UpdateRepository(addressToEdit);

            this.parentsRepository.Update(parentToEdit);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ParentServiceModel> GetParentById(int id)
        {
            var parent = await this.parentsRepository.All()
                                            .To<ParentServiceModel>()
                                            .Include(u => u.User)
                                            .Include(a => a.Address)
                                            .ThenInclude(d => d.CurrentDistrict)
                                            .Include(a => a.Address)
                                            .ThenInclude(d => d.PermanentDistrict)
                                            .Include(w => w.WorkDistrict)
                                            .SingleOrDefaultAsync(p => p.Id == id);

            //AddressDetailsServiceModel address = (await this.addressesService.GetAddressDetailsById(parent.AddressId))
            //                                        .To<AddressDetailsServiceModel>();

            //if (address == null)
            //{
            //    throw new ArgumentNullException(string.Format(GlobalConstants.NullReferenceAddressId, address.Id));
            //}

            //DistrictServiceModel workDistrict = await this.districtsService.GetDistrictById(parent.WorkDistrictId);

            //parent.WorkDistrict = workDistrict;
            //parent.Address = address;

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
                throw new ArgumentNullException(string.Format(GlobalConstants.NullReferenceParentId, id));
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

            string role = null;
            if (exstParent != null)
            {
                role = exstParent.Role.ToString();
            }

            return role;
        }
    }
}
