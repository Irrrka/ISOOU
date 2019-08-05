namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;

    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Districts;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.EntityFrameworkCore;

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<ClassProfile> classProfileRepository;
        private readonly IDistrictsService districtsService;

        public SchoolsService(
                IRepository<School> schoolRepository,
                IRepository<ClassProfile> classProfileRepository,
                IDistrictsService districtsService)
        {
            this.schoolRepository = schoolRepository;
            this.classProfileRepository = classProfileRepository;
            this.districtsService = districtsService;
        }

        public async Task<IQueryable<SchoolServiceModel>> GetAllSchoolsByDistrictId(int id)
        {
            var currDistrict = await this.districtsService
                                        .GetDistrictById(id);

            if (currDistrict == null)
            {
                throw new NullReferenceException();
            }

            var schools = this.schoolRepository
                .All()
                .Where(d => d.District.Name == currDistrict.Name)
                .To<SchoolServiceModel>();

            return schools;
        }

        public IQueryable<SchoolServiceModel> GetAllSchools()
        {
            var schools = this.schoolRepository
                .All()
                .To<SchoolServiceModel>();

            return schools;
        }

        public async Task<SchoolServiceModel> GetSchoolDetailsById(int id)
        {
            var schoolDetails = await this.schoolRepository
                .All()
                .To<SchoolServiceModel>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (schoolDetails == null)
            {
                throw new NullReferenceException();
            }

            return schoolDetails;
        }

        public IQueryable<ClassProfileServiceModel> GetAllClassProfiles()
        {
            var classProfiles = this.classProfileRepository
                .All()
                .To<ClassProfileServiceModel>();

            return classProfiles;
        }
    }
}
