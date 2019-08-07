namespace ISOOU.Services.Data
{
    using System;
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
        //TODO Refactor Repository!!!
        private readonly IRepository<SystemUser> usersRepository;
        private readonly IRepository<Parent> parentsRepository;
        private readonly IRepository<AddressDetails> addressesRepository;
        private readonly IRepository<District> districtsRepository;


        public ParentsService(
            IRepository<SystemUser> usersRepository,
            IRepository<Parent> parentsRepository,
            IRepository<AddressDetails> addressesRepository,
            IRepository<District> districtsRepository)
        {
            this.usersRepository = usersRepository;
            this.parentsRepository = parentsRepository;
            this.addressesRepository = addressesRepository;
            this.districtsRepository = districtsRepository;
        }

        public async Task<bool> Create(string userIdentity, ParentServiceModel parentServiceModel)
        {
            if (parentServiceModel == null)
            {
                throw new ArgumentNullException();
            }

            SystemUser user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.UserName == userIdentity);

            var parent = parentServiceModel.To<Parent>();
            parent.User = user;

            await this.parentsRepository.AddAsync(parent);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<ParentServiceModel> GetParents()
        {
            var parents = this.parentsRepository
                .All()
                .Include(x => x.User)
                .To<ParentServiceModel>();

            return parents;
        }

        public async Task<bool> Edit(string userIdentity, ParentServiceModel parentServiceModel)
        {
            var parentToEdit = await this.parentsRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Id);

            if (parentToEdit == null)
            {
                throw new ArgumentNullException();
            }

            SystemUser user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.UserName == userIdentity);
            parentToEdit.User = user;
            parentToEdit.UCN = parentToEdit.UCN;
            parentToEdit.Role = parentToEdit.Role;

            parentToEdit.WorkDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.WorkDistrict.Id);

            if (parentToEdit.WorkDistrict == null)
            {
                throw new ArgumentNullException();
            }

            var addressToEdit = (await this.parentsRepository
                                .All()
                                .Include(a => a.Address)
                                .SingleOrDefaultAsync(a => a.Id == parentServiceModel.Id)).Address;

            if (addressToEdit == null)
            {
                throw new ArgumentNullException();
            }

            addressToEdit.Current = parentServiceModel.Address.Current;
            addressToEdit.Permanent = parentServiceModel.Address.Permanent;
            addressToEdit.CurrentCity = parentServiceModel.Address.CurrentCity;
            addressToEdit.PermanentCity = parentServiceModel.Address.PermanentCity;
            addressToEdit.CurrentDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Address.CurrentDistrict.Id);
            if (addressToEdit.CurrentDistrict == null)
            {
                throw new ArgumentNullException();
            }
            addressToEdit.PermanentDistrict = await this.districtsRepository
                                .All()
                                .SingleOrDefaultAsync(p => p.Id == parentServiceModel.Address.PermanentDistrict.Id);
            if (addressToEdit.PermanentDistrict == null)
            {
                throw new ArgumentNullException();
            }
            parentToEdit.Address = addressToEdit;

            this.addressesRepository.Update(addressToEdit);
            await this.addressesRepository.SaveChangesAsync();

            this.parentsRepository.Update(parentToEdit);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result>0;
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

            if (parentToDelete==null)
            {
                throw new ArgumentNullException();
            }

            parentToDelete.IsDeleted = true;
            this.parentsRepository.Delete(parentToDelete);
            var result = await this.parentsRepository.SaveChangesAsync();

            return result>0;
        }
    }
}
