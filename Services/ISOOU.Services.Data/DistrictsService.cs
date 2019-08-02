namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class DistrictsService : IDistrictsService
    {
        private readonly IRepository<District> districtRepository;

        public DistrictsService(IRepository<District> districtRepository)
        {
            this.districtRepository = districtRepository;
        }

        public async Task<DistrictServiceModel> GetDistrictById(int id)
        {
            var district = await this.districtRepository
                .All()
                .Where(d => d.Id == id)
                .To<DistrictServiceModel>()
                .FirstOrDefaultAsync();

            if (district == null)
            {
                throw new NullReferenceException();
            }

            return district;
        }

        public IQueryable<DistrictServiceModel> GetAllDistrictsAsync()
        {
            var districts = this.districtRepository
                                .All()
                                .To<DistrictServiceModel>();

            if (districts == null)
            {
                throw new NullReferenceException();
            }

            return districts;
        }
    }
}
