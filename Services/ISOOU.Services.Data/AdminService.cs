namespace ISOOU.Services.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class AdminService : IAdminService
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<Question> questionsRepository;

        public AdminService(
            IRepository<School> schoolRepository,
            IRepository<Candidate> candidatesRepository,
            IRepository<Question> questionsRepository)
        {
            this.schoolRepository = schoolRepository;
            this.candidatesRepository = candidatesRepository;
            this.questionsRepository = questionsRepository;
        }

        public async Task<QuestionServiceModel> ReadLastMessage()
        {
            var message = await this.questionsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .To<QuestionServiceModel>()
                .FirstOrDefaultAsync();

            return message;
        }

        public async Task<Dictionary<School, List<SchoolCandidate>>> StartAdmissionProcedure()
        {
            var schoolsFromDb = await this.schoolRepository.All()
                                                     .ToListAsync();
            if (schoolsFromDb == null)
            {
                throw new NullReferenceException();
            }

            var candidatesFromDb = await this.candidatesRepository.All()
                                         .ToListAsync();
            if (candidatesFromDb == null)
            {
                throw new NullReferenceException();
            }

            var dataForProcedure =
                new Dictionary<School, List<SchoolCandidate>>();

            foreach (var schoolFromDb in schoolsFromDb)
            {
                if (!dataForProcedure.ContainsKey(schoolFromDb))
                {
                    dataForProcedure.Add(schoolFromDb, new List<SchoolCandidate>());
                }

                foreach (var candidateFromDb in candidatesFromDb)
                {
                    var candidateForSchool = candidateFromDb.SchoolCandidates
                        .Where(x => x.SchoolId == schoolFromDb.Id && x.CandidateId == candidateFromDb.Id)
                        .FirstOrDefault();
                    if (candidateForSchool != null)
                    {
                        dataForProcedure[schoolFromDb].Add(candidateForSchool);
                    }
                }
            }

            return dataForProcedure;
        }
    }
}
