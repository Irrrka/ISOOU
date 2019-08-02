namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
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
        private readonly IRepository<District> districtsRepository;

        public ClaimsPrincipal User { get; private set; }

        public ParentsService(
            UserManager<SystemUser> userManager,
            IRepository<Parent> parentsRepository,
            IRepository<AddressDetails> addressesRepository,
            IRepository<District> districtsRepository)
        {
            this.userManager = userManager;
            this.parentsRepository = parentsRepository;
            this.addressesRepository = addressesRepository;
            this.districtsRepository = districtsRepository;
        }

        public async Task<bool> Create(ParentServiceModel parentServiceModel)
        {
            CoreValidator.EnsureNotNull(parentServiceModel, GlobalConstants.ParentNotFound);

            District workDistrictFromDb =
                this.districtsRepository
                .All()
                .SingleOrDefault(d => d.Name == parentServiceModel.WorkDistrict.Name);
            District currentDistrictFromDb =
                this.districtsRepository
                .All()
                .SingleOrDefault(d => d.Name == parentServiceModel.Address.CurrentDistrict.Name);
            District permanentDistrictFromDb =
               this.districtsRepository
               .All()
               .SingleOrDefault(d => d.Name == parentServiceModel.Address.PermanentDistrict.Name);

            AddressDetails addressDetailsForDb = new AddressDetails
            {
                PermanentCity = parentServiceModel.Address.PermanentCity,
                PermanentDistrict = permanentDistrictFromDb,
                Permanent = parentServiceModel.Address.Permanent,
                Current = parentServiceModel.Address.Permanent,
                CurrentCity = parentServiceModel.Address.CurrentCity,
                CurrentDistrict = currentDistrictFromDb,
            };

            var parent = AutoMapper.Mapper.Map<Parent>(parentServiceModel);
            parent.WorkDistrict = workDistrictFromDb;
            parent.Address = addressDetailsForDb;
            parent.User = await this.userManager.GetUserAsync(this.User);

            await this.parentsRepository.AddAsync(parent);
            await this.parentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IQueryable<ParentServiceModel>> GetParents()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            CoreValidator.EnsureNotNull(user, GlobalConstants.UserNotFound);

            var parents = this.parentsRepository
                .All()
                .Where(u => u.User == user)
                .To<ParentServiceModel>();

            return parents;
        }

        public async Task<bool> Edit(ParentServiceModel parentServiceModel)
        {
            var parentToEdit = await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Id);
            CoreValidator.EnsureNotNull(GlobalConstants.ParentNotFound);

            parentToEdit.FirstName = parentServiceModel.FirstName;
            parentToEdit.MiddleName = parentServiceModel.MiddleName;
            parentToEdit.LastName = parentServiceModel.LastName;
            parentToEdit.PhoneNumber = parentServiceModel.PhoneNumber;
            parentToEdit.WorkName = parentServiceModel.WorkName;

            parentToEdit.WorkDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.WorkDistrict.Id);
            var addressToEdit = await this.addressesRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Address.Id);
            CoreValidator.EnsureNotNull(GlobalConstants.AddressNotFound);

            addressToEdit.Current = parentServiceModel.Address.Current;
            addressToEdit.Permanent = parentServiceModel.Address.Permanent;
            addressToEdit.CurrentCity = parentServiceModel.Address.CurrentCity;
            addressToEdit.PermanentCity = parentServiceModel.Address.PermanentCity;
            addressToEdit.CurrentDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Address.CurrentDistrict.Id);
            addressToEdit.PermanentDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Address.PermanentDistrict.Id);

            parentToEdit.Address = addressToEdit;

            this.addressesRepository.Update(addressToEdit);
            await this.addressesRepository.SaveChangesAsync();

            this.parentsRepository.Update(parentToEdit);
            await this.parentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<ParentServiceModel> GetParentById(int id)
        {
            var parent = await this.parentsRepository
                                .All()
                                .To<ParentServiceModel>()
                                .SingleOrDefaultAsync(p => p.Id == id);
            CoreValidator.EnsureNotNull(GlobalConstants.ParentNotFound);
            return parent;
        }

        public async Task<bool> Delete(int id)
        {
            var parentToDelete = await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == id);
            CoreValidator.EnsureNotNull(GlobalConstants.ParentNotFound);

            parentToDelete.IsDeleted = true;
            this.parentsRepository.Update(parentToDelete);
            await this.parentsRepository.SaveChangesAsync();

            return true;
        }
    }
}
