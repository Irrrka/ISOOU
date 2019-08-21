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
    using Microsoft.EntityFrameworkCore;

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<SystemUser> userRepository;
        private readonly IDistrictsService districtsService;
        private readonly IRepository<CandidateApplication> candidateApplicationRepository;

        public SchoolsService(
                IRepository<School> schoolRepository,
                IRepository<CandidateApplication> candidateApplicationRepository,
                IRepository<SystemUser> userRepository,
                IDistrictsService districtsService)
        {
            this.schoolRepository = schoolRepository;
            this.userRepository = userRepository;
            this.districtsService = districtsService;
            this.candidateApplicationRepository = candidateApplicationRepository;
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
                .Include(c=>c.Candidates)
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

        public async Task<int> GetSchoolIdByName(string name)
        {
            var schoolId = (await this.schoolRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name)).Id;

            if (schoolId == 0)
            {
                throw new ArgumentNullException(nameof(schoolId));
            }

            return schoolId;
        }

        public async Task<SchoolServiceModel> GetSchoolForEdit(string userIdentity)
        {
            SystemUser user = await this.userRepository
                           .All()
                           .FirstOrDefaultAsync(x => x.UserName == userIdentity);

            School schoolToEdit = await this.schoolRepository
                               .All()
                               .FirstOrDefaultAsync(s => s.Id == user.AdmissionSchoolId);
            var districtFromDb = await this.districtsService.GetDistrictById(schoolToEdit.DistrictId);

            if (schoolToEdit == null)
            {
                throw new ArgumentNullException();
            }

            var model = schoolToEdit.To<SchoolServiceModel>();
            model.District = districtFromDb;
            return model;
        }

        public async Task<bool> EditSchool(int id, SchoolServiceModel model)
        {
            District districtFromDb =
                (await this.districtsService.GetDistrictByName(model.District.Name)).To<District>();

            if (districtFromDb == null)
            {
                throw new ArgumentNullException(nameof(districtFromDb));
            }

            School schoolFromDb = (await this.GetSchoolDetailsById(id)).To<School>();

            if (schoolFromDb == null)
            {
                throw new ArgumentNullException(nameof(schoolFromDb));
            }

            schoolFromDb.Name = model.Name;
            schoolFromDb.Address = model.Address;
            schoolFromDb.DirectorName = model.DirectorName;
            schoolFromDb.District = districtFromDb;
            schoolFromDb.Email = model.Email;
            schoolFromDb.FreeSpots = model.FreeSpots;
            schoolFromDb.PhoneNumber = model.PhoneNumber;
            schoolFromDb.URLOfMap = model.URLOfMap;
            schoolFromDb.URLOfSchool = model.URLOfSchool;

            this.schoolRepository.Update(schoolFromDb);
            var result = await this.schoolRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<string>> GetAdmittedCandidates()
        {

            var schools = await this.schoolRepository.All()
                             .ToListAsync();

            if (schools == null)
            {
                throw new ArgumentNullException(nameof(schools));
            }

            var admitted = new List<string>();

           

            return admitted;
        }

        public IQueryable<CandidateApplicationServiceModel> GetSchoolsAndCandidates()
        {
            IQueryable<CandidateApplicationServiceModel> result = this.candidateApplicationRepository.All()
                .To<CandidateApplicationServiceModel>();

            return result;
        }
    }
}
