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
        private readonly IRepository<CandidateApplication> candidateApplicationRepository;
        private readonly IRepository<AdmissionProcedure> admissionProcedureRepository;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<Question> questionsRepository;
        private readonly ICalculatorService calculatorService;

        public AdminService(
            IRepository<School> schoolRepository,
            IRepository<CandidateApplication> candidateApplicationRepository,
            IRepository<Candidate> candidatesRepository,
            IRepository<Question> questionsRepository,
            ICalculatorService calculatorService,
            IRepository<AdmissionProcedure> admissionProcedureRepository)
        {
            this.schoolRepository = schoolRepository;
            this.candidateApplicationRepository = candidateApplicationRepository;
            this.candidatesRepository = candidatesRepository;
            this.questionsRepository = questionsRepository;
            this.calculatorService = calculatorService;
            this.admissionProcedureRepository = admissionProcedureRepository;
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

        public async Task<bool> AdmissionProcedure()
        {
            AdmissionProcedure admissionProcedure = new AdmissionProcedure
            {
                StartApplyDocuments = DateTime.Today.AddDays(-10),
                EndApplyDocuments = DateTime.Today.AddDays(-1),
                RankingDate = DateTime.Today,
                StartEnrollment = DateTime.Today.AddDays(1),
                EndEnrollment = DateTime.Today.AddDays(8),
                Status = AdmissionProcedureStatus.Finished,
                Year = DateTime.Now.Year,
                ParticipatedCandidates = new List<CandidateApplication>(),
            };

            await this.admissionProcedureRepository.AddAsync(admissionProcedure);
            await this.admissionProcedureRepository.SaveChangesAsync();

            var result = await this.StartAdmissionProcedure(admissionProcedure);

            //admissionProcedure.ParticipatedCandidates = await this.FinishAdmissionProcedure(admissionProcedure.ParticipatedCandidates.ToList());

            return result;
        }

        public string GetProcedureStatus()
        {
            var status = this.admissionProcedureRepository.All()
                .OrderByDescending(d => d.Id)
                .Select(s => s.Status)
                .FirstOrDefault().ToString();

            return status;
        }

        public async Task<bool> RevertAdmissionProcedure()
        {
            var procedure = await this.admissionProcedureRepository.All()
                .LastOrDefaultAsync();
            procedure.Status = AdmissionProcedureStatus.Waiting;
            this.admissionProcedureRepository.Update(procedure);
            var result = await this.admissionProcedureRepository.SaveChangesAsync();

            return result > 0;
        }

        private async Task<bool> StartAdmissionProcedure(AdmissionProcedure admissionProcedure)
        {
            int result = 0;
            List<School> schoolsFromDb = await this.schoolRepository.All()
                                                                .Include(c => c.Candidates)
                                                                .ToListAsync();
            if (schoolsFromDb == null)
            {
                throw new NullReferenceException();
            }

            foreach (var school in schoolsFromDb)//107
            {
                var candidates = this.candidateApplicationRepository.All()
                                     .Where(sc => sc.SchoolId == school.Id).ToList();//qvor kalin

                int freeSpots = school.FreeSpots;//35

                if (candidates.Count != 0)//2
                {
                    foreach (var candidate in candidates)//qvor kalin
                    {
                        var totalScores = this.calculatorService
                            .CalculateTotalScoreForTheAdmissionProcedure(candidate.CandidateId, school.Id);//15 13

                        candidate.TotalScoresForSchool = totalScores;
                        this.candidateApplicationRepository.Update(candidate);
                        await this.candidateApplicationRepository.SaveChangesAsync();
                    }

                    var admittedcandidates = candidates.OrderByDescending(x => x.TotalScoresForSchool).Take(freeSpots).ToList();
                    admittedcandidates.ForEach(s => s.Candidate.Status = CandidateStatus.Admitted);

                    var notadmittedcandidates = candidates.OrderByDescending(x => x.TotalScoresForSchool).Skip(freeSpots).ToList();
                    notadmittedcandidates.ForEach(s => s.Candidate.Status = CandidateStatus.NotAdmitted);

                    admissionProcedure.ParticipatedCandidates.AddRange(admittedcandidates);
                    admissionProcedure.ParticipatedCandidates.AddRange(notadmittedcandidates);
                }

                this.admissionProcedureRepository.Update(admissionProcedure);
                result = await this.admissionProcedureRepository.SaveChangesAsync();
            }

            var participatedCandidates = admissionProcedure.ParticipatedCandidates;

            return result > 0;
        }

        private async Task<List<CandidateApplication>> FinishAdmissionProcedure(List<CandidateApplication> participatedCandidates)
        {
            foreach (var candidate in participatedCandidates)
            {
                var schoolId = candidate.SchoolId;
                var freeSpots = (await this.schoolRepository.All().Where(s => s.Id == schoolId).FirstOrDefaultAsync()).FreeSpots;

                var admitted = participatedCandidates.Take(freeSpots).ToList();
                candidate.Candidate.Status = CandidateStatus.Admitted;

                var notAdmitted = participatedCandidates.Skip(freeSpots).ToList();
                candidate.Candidate.Status = CandidateStatus.NotAdmitted;
            }

            return participatedCandidates;
        }

    }
}
