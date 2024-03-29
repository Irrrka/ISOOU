﻿using ISOOU.Common;
using ISOOU.Data.Common.Repositories;
using ISOOU.Data.Models;
using ISOOU.Services.Data.Contracts;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Services.Data
{
    public class AddressesService : IAddressesService
    {
        private readonly IRepository<AddressDetails> addressesRepository;

        public AddressesService(IRepository<AddressDetails> addressesRepository)
        {
            this.addressesRepository = addressesRepository;
        }

        public async Task<AddressDetailsServiceModel> GetAddressDetailsById(int id)
        {
            AddressDetailsServiceModel address = (await this.addressesRepository.All()
                                                    .FirstOrDefaultAsync(a => a.Id == id))
                                                    .To<AddressDetailsServiceModel>();
            if (address == null)
            {
                throw new ArgumentNullException(string.Format(GlobalConstants.NullReferenceAddressId, id));
            }

            return address;
        }

        public async Task<bool> UpdateRepository(AddressDetails addressToEdit)
        {
            this.addressesRepository.Update(addressToEdit);
            var result = await this.addressesRepository.SaveChangesAsync();
            return result > 0;
        }

    }
}
