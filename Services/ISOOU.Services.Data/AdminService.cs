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
    using ISOOU.Services.Models;
    using ISOOU.Services.Mapping;

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

        public async Task<Dictionary<School, Dictionary<ClassProfile, List<Candidate>>>> StartAdmissionProcedure()
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
                new Dictionary<School, Dictionary<ClassProfile, List<Candidate>>>();

            foreach (var schoolFromDb in schoolsFromDb)
            {
                if (!dataForProcedure.ContainsKey(schoolFromDb))
                {
                    dataForProcedure.Add(schoolFromDb, new Dictionary<ClassProfile, List<Candidate>>());
                }

                foreach (var schoolClass in schoolFromDb.SchoolClasses)
                {
                    if (!dataForProcedure[schoolFromDb].ContainsKey(schoolClass.Class.Profile))
                    {
                        dataForProcedure[schoolFromDb].Add(schoolClass.Class.Profile, new List<Candidate>());
                    }

                    var candidates = new List<Candidate>();
                    //TODO check!!!
                    foreach (var candidateFromDb in candidatesFromDb)
                    {
                        var result = candidateFromDb.CandidateSchoolClasses
                            .Where(x => x.SchoolClass.School == schoolFromDb && x.SchoolClass.Class == schoolClass.Class)
                            .FirstOrDefault(x => x.Candidate == candidateFromDb).Candidate;
                        candidates.Add(result);
                    }

                    dataForProcedure[schoolFromDb][schoolClass.Class.Profile].AddRange(candidates);
                }
            }

            return dataForProcedure;
        }
    }
}
