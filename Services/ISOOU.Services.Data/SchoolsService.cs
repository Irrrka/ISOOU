namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ISOOU.Common;
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
                throw new NullReferenceException(string.Format(GlobalConstants.NullReferenceDistrictId, id));
            }

            var schools = this.schoolRepository
                .All()
                .Where(d => d.District.Name == currDistrict.Name)
                .To<SchoolServiceModel>();

            return schools;
        }

        public IQueryable<SchoolServiceModel> GetAllSchools()
        {
            var schools = this.schoolRepository.All()
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
                throw new NullReferenceException(string.Format(GlobalConstants.NullReferenceSchoolId, id));
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
                throw new NullReferenceException(string.Format(GlobalConstants.NullReferenceSchoolId, schoolId));
            }

            return schoolId;
        }

        public async Task<SchoolServiceModel> GetSchoolForEdit(string userIdentity)
        {
            //TODO with userManager
            SystemUser user = await this.userRepository
                           .All()
                           .FirstOrDefaultAsync(x => x.UserName == userIdentity);

            School schoolToEdit = await this.schoolRepository
                               .All()
                               .FirstOrDefaultAsync(s => s.Id == user.DirectorsSchoolId);
            //var districtFromDb = await this.districtsService.GetDistrictById(schoolToEdit.DistrictId);

            if (schoolToEdit == null)
            {
                throw new NullReferenceException(string.Format(GlobalConstants.NullReferenceSchoolId, schoolToEdit.Id));
            }

            var model = schoolToEdit.To<SchoolServiceModel>();
            //model.District = districtFromDb;
            return model;
        }

        public async Task<bool> EditSchool(int id, SchoolServiceModel model)
        {
            var schoolFromDb = await this.schoolRepository
               .All()
               .FirstOrDefaultAsync(x => x.Id == id);

            if (schoolFromDb == null)
            {
                throw new NullReferenceException(string.Format(GlobalConstants.NullReferenceSchoolId, id));
            }

            schoolFromDb.Name = model.Name;
            schoolFromDb.Address = schoolFromDb.Address;
            schoolFromDb.DirectorName = model.DirectorName;
            schoolFromDb.District = schoolFromDb.District;
            schoolFromDb.Email = model.Email;
            schoolFromDb.FreeSpots = model.FreeSpots;
            schoolFromDb.PhoneNumber = model.PhoneNumber;
            schoolFromDb.URLOfMap = schoolFromDb.URLOfMap;
            schoolFromDb.URLOfSchool = model.URLOfSchool;

            this.schoolRepository.Update(schoolFromDb);
            var result = await this.schoolRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<string>> GetAdmittedCandidates()
        {

            var schools = await this.schoolRepository.All()
                             .Include(c => c.Candidates)
                             .ToListAsync();

            if (schools == null)
            {
                throw new NullReferenceException(nameof(schools));
            }

            var admitted = new List<string>();

            foreach (var school in schools)
            {
                var candidates = school.Candidates.Where(c => c.Candidate.Status == CandidateStatus.Admitted).ToList();
                admitted.AddRange(candidates.Select(n => n.Candidate.FullName));
            }

            return admitted;
        }

        public IQueryable<CandidateApplicationServiceModel> GetSchoolsAndCandidates()
        {
            IQueryable<CandidateApplicationServiceModel> result =
                            this.candidateApplicationRepository.All()
                .To<CandidateApplicationServiceModel>();

            return result;
        }
    }
}
