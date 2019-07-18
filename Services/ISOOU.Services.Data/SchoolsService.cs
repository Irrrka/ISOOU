﻿namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ISOOU.Data;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.EntityFrameworkCore;

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<ISOOUDbContext> allRepository;
        private readonly IRepository<Class> classRepository;

        private readonly IDistrictsService districtsService;

        public SchoolsService(IRepository<ISOOUDbContext> allRepository, IRepository<School> schoolRepository, IRepository<Class> classRepository, IDistrictsService districtsService)
        {
            this.allRepository = allRepository;
            this.schoolRepository = schoolRepository;
            this.classRepository = classRepository;
            this.districtsService = districtsService;
        }

        public async Task<IEnumerable<SchoolViewModel>> GetAllSchoolsByDistrictValue(int value)
        {
            DistrictViewModel currDistrict = await this.districtsService
                                        .GetDistrictByValue<DistrictViewModel>(value);

            var schools = await this.schoolRepository
                .All()
                .Where(d => d.Address.District.Name == currDistrict.Name)
                .To<SchoolViewModel>()
                .ToListAsync();

            return schools;
        }

        public async Task<IEnumerable<SchoolClassesViewModel>> GetAllSchoolsByDistrictName(string districtName)
        {
            var schools = await this.schoolRepository
                .All()
                .Where(d => d.Address.District.Name == districtName)
                .To<SchoolClassesViewModel>()
                .ToListAsync();

            return schools;
        }

        public async Task<IEnumerable<AllSchoolsViewModel>> GetAllSchoolsAsync()
        {
            var schools = await this.schoolRepository.All()
                .To<AllSchoolsViewModel>()
                .OrderBy(d => d.Name)
                .ToListAsync();

            return schools;
        }

        public async Task<BaseSchoolModel> GetSchoolByName(string name)
        {
            var school = await this.schoolRepository
                .All()
                .To<BaseSchoolModel>()
                .FirstOrDefaultAsync(sch => sch.Name == name);

            return school;
        }

        public async Task<BaseSchoolModel> GetSchoolById(int id)
        {
            var school = await this.schoolRepository
                .All()
                .To<BaseSchoolModel>()
                .FirstOrDefaultAsync(sch => sch.Id == id);

            return school;
        }

        public IEnumerable<ClassViewModel> GetAllClasses()
        {
            var classes = this.classRepository.All();
            var classesViewModel = classes.To<ClassViewModel>().ToList();

            return classesViewModel;
        }

        public async Task<SchoolDetailsWithSpotsAndCandidatesViewModel> GetSchoolsDetailsForSpotsAndCandidates(BaseSchoolModel school)
        {
            var schoolModel = await this.schoolRepository
               .All()
               .To<SchoolDetailsWithSpotsAndCandidatesViewModel>()
               .FirstOrDefaultAsync(sch => sch.Id == school.Id);

            schoolModel.PossibleYears = FreeSpots.GetAllPossibleYears().ToList();
            schoolModel.AdmissionProcedureStatus = AdmissionProcedureStatus.Finished.ToString();
            schoolModel.AdmittedCandidatesUniqueNumber = new List<string> { "YM", "KM" };
            schoolModel.NotAdmittedCandidatesUniqueNumber = new List<string> { "IM", "IM" };
            schoolModel.AdmissionProcedureStatus = AdmissionProcedureStatus.Finished.ToString();
            
            
            //SchoolDetailsWithSpotsAndCandidatesViewModel model =
            //    new SchoolDetailsWithSpotsAndCandidatesViewModel
            //        {
            //        Name = schoolFromDb.Name,
            //        AddressPermanent = schoolFromDb.Address.Permanent,
            //        DistrictName = schoolFromDb.District.Name,
            //        DirectorName = schoolFromDb.DirectorName,
            //        PhoneNumber = schoolFromDb.PhoneNumber,
            //        Email = schoolFromDb.Email,
            //        URLOfMap = schoolFromDb.URLOfMap,
            //        URLOfSchool = schoolFromDb.URLOfSchool,
            //        AdmissionProcedureStatus = schoolFromDb.AdmissionProcedure.Status.ToString(),
            //        PossibleYears = FreeSpots.GetAllPossibleYears().ToList(),
            //        };
            return schoolModel;
        }
    }
}
