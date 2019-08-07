namespace ISOOU.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
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
            var districtFromDb = await this.districtRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);
            //TODO Map?
            DistrictServiceModel district = new DistrictServiceModel();
            district.Id = districtFromDb.Id;
            district.Name = districtFromDb.Name;

            if (district == null)
            {
                throw new NullReferenceException();
            }

            return district;
        }

        public async Task<DistrictServiceModel> GetDistrictByName(string name)
        {
            var districtFromDb = await this.districtRepository
                .All()
                .FirstOrDefaultAsync(d => d.Name == name);
            //TODO Map?
            DistrictServiceModel district = new DistrictServiceModel();
            district.Id = districtFromDb.Id;
            district.Name = districtFromDb.Name;

            if (district == null)
            {
                throw new NullReferenceException();
            }

            return district;
        }

        public IQueryable<DistrictServiceModel> GetAllDistricts()
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
