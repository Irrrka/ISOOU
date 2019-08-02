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
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.EntityFrameworkCore;

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IDistrictsService districtsService;

        public SchoolsService(
                IRepository<School> schoolRepository,
                IDistrictsService districtsService)
        {
            this.schoolRepository = schoolRepository;
            this.districtsService = districtsService;
        }

        public async Task<IEnumerable<SchoolViewModel>> GetAllSchoolsByDistrictId(int id)
        {
            var currDistrict = await this.districtsService
                                        .GetDistrictById(id);

            var schools = await this.schoolRepository
                .All()
                .Where(d => d.District.Name == currDistrict.Name)
                .To<SchoolViewModel>()
                .ToListAsync();

            if (schools == null)
            {
                throw new NullReferenceException();
            }

            return schools;
        }

        //public async Task<IEnumerable<SchoolClassViewModel>> GetAllSchoolsByDistrictName(string districtName)
        //{

        //    var schoolsWithClassesAndFreeSpots = await this.schoolRepository
        //        .All()
        //        .Where(d => d.District.Name == districtName)
        //        .To<SchoolClassViewModel>()
        //        .ToListAsync();

        //    if (schoolsWithClassesAndFreeSpots == null)
        //    {
        //        throw new NullReferenceException();
        //    }

        //    return schoolsWithClassesAndFreeSpots;
        //}

        public async Task<IEnumerable<SchoolClassViewModel>> GetAllSchoolsByDistrictNameWithClassesAndFreeSpots(string name)
        {
            var schoolsWithClassesAndFreeSpots = await this.schoolRepository
                .All()
                .Where(d => d.District.Name == name)
                .To<SchoolClassViewModel>()
                .ToListAsync();

            if (schoolsWithClassesAndFreeSpots == null)
            {
                throw new NullReferenceException();
            }

            return schoolsWithClassesAndFreeSpots;
        }

        public async Task<IEnumerable<AllSchoolsViewModel>> GetAllSchoolsAsync()
        {
            var schools = await this.schoolRepository.All()
                .To<AllSchoolsViewModel>()
                .ToListAsync();

            if (schools == null)
            {
                throw new NullReferenceException();
            }

            return schools;
        }

        public async Task<IEnumerable<AdmissionProcedureSchoolViewModel>> GetSchoolsForAdmissionProcedureAsync()
        {
            var schools = await this.schoolRepository.All()
                .To<AdmissionProcedureSchoolViewModel>()
                .ToListAsync();

            if (schools == null)
            {
                throw new NullReferenceException();
            }

            return schools;
        }

        //public async Task<BaseSchoolModel> GetSchoolByName(string name)
        //{
        //    var school = await this.schoolRepository
        //        .All()
        //        .To<BaseSchoolModel>()
        //        .FirstOrDefaultAsync(sch => sch.Name == name);

        //    if (school == null)
        //    {
        //        throw new NullReferenceException();
        //    }

        //    return school;
        //}

        //public async Task<BaseSchoolModel> GetSchoolById(int id)
        //{
        //    var school = await this.schoolRepository
        //        .All()
        //        .To<BaseSchoolModel>()
        //        .FirstOrDefaultAsync(sch => sch.Id == id);

        //    if (school == null)
        //    {
        //        throw new NullReferenceException();
        //    }

        //    return school;
        //}

        public async Task<SchoolDetails> GetSchoolDetailsById(int id)
        {
            SchoolDetails schoolDetails = await this.schoolRepository
                .All()
                .To<SchoolDetails>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (schoolDetails == null)
            {
                throw new NullReferenceException();
            }

            return schoolDetails;
        }
    }
}
