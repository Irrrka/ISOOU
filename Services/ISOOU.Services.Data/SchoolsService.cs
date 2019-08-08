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
        private readonly IRepository<Class> classRepository;
        private readonly IRepository<SchoolClass>schoolClassRepository;
        private readonly IDistrictsService districtsService;

        public SchoolsService(
                IRepository<School> schoolRepository,
                IRepository<ClassProfile> classProfileRepository,
                IRepository<Class> classRepository,
                IRepository<SchoolClass> schoolClassRepository,
                IDistrictsService districtsService)
        {
            this.schoolRepository = schoolRepository;
            this.classProfileRepository = classProfileRepository;
            this.classRepository = classRepository;
            this.schoolClassRepository = schoolClassRepository;
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

        public async Task<SchoolServiceModel> GetSchoolDetailsByName(string name)
        {
            var schoolDetails = await this.schoolRepository
                .All()
                .To<SchoolServiceModel>()
                .FirstOrDefaultAsync(x => x.Name == name);

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

        public SchoolClassServiceModel GetSchoolClassBySchoolAndClass(string schoolName, string classProfile)
        {
            var schoolClass = this.schoolClassRepository
                .All()
                .To<SchoolClassServiceModel>()
                .Where(c => c.Class.Profile.Name == classProfile)
                .FirstOrDefault(s => s.School.Name == schoolName);

            return schoolClass;
        }

        public IQueryable<ClassServiceModel> GetAllClasses()
        {
            var classes = this.classRepository
                .All()
                .To<ClassServiceModel>();

            return classes;
        }

        public async Task<bool> CreateClassProfile(string name)
        {
            var classProfile = new ClassProfile { Name = name };
            await this.classProfileRepository.AddAsync(classProfile);
            var result = await this.classProfileRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditSchool(ClassServiceModel classModel, SchoolServiceModel model)
        {
            var classfORdb = new Class
            {
                Profile = classModel.Profile.To<ClassProfile>(),
                InitialFreeSpots = classModel.InitialFreeSpots,
            };
            await this.classRepository.AddAsync(classfORdb);
            await this.classRepository.SaveChangesAsync();

            var schoolToEdit = model.To<School>();

            SchoolClass schoolClass = new SchoolClass
            {
                Class = classfORdb,
                ClassId = classfORdb.Id,
                School = model.To<School>(),
                SchoolId = model.To<School>().Id,
            };

            await this.schoolClassRepository.AddAsync(schoolClass);
            await this.schoolClassRepository.SaveChangesAsync();

            this.schoolRepository.Update(schoolToEdit);
            var result = await this.schoolRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
