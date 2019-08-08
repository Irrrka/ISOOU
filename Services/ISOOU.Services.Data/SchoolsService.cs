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

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<SystemUser> userRepository;
        private readonly IDistrictsService districtsService;

        public SchoolsService(
                IRepository<School> schoolRepository,
                IRepository<SystemUser> userRepository,
                IDistrictsService districtsService)
        {
            this.schoolRepository = schoolRepository;
            this.userRepository = userRepository;
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

        //public async Task<bool> CreateClassProfile(string name)
        //{
        //    var classProfile = new ClassProfile { Name = name };
        //    await this.classProfileRepository.AddAsync(classProfile);
        //    var result = await this.classProfileRepository.SaveChangesAsync();

        //    return result > 0;
        //}

        public async Task<SchoolServiceModel> GetSchoolForEdit(string userIdentity)
        {
            SystemUser user = await this.userRepository
                           .All()
                           .FirstOrDefaultAsync(x => x.UserName == userIdentity);

            School schoolToEdit = await this.schoolRepository
                               .All()
                               .FirstOrDefaultAsync(s => s.Id == user.AdmissionSchoolId);
            if (schoolToEdit == null)
            {
                throw new ArgumentNullException();
            }

            var model = schoolToEdit.To<SchoolServiceModel>();

            return model;
        }

        public async Task<bool> EditSchool(SchoolServiceModel model)
        {
            var schoolToEdit = model.To<School>();

            if (schoolToEdit.District == null)
            {
                throw new ArgumentNullException();
            }

            this.schoolRepository.Update(schoolToEdit);
            var result = await this.schoolRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
