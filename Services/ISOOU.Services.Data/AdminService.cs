﻿namespace ISOOU.Services.Data
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

        public async Task<bool> AdmissionProcedure()
        {
            AdmissionProcedure admissionProcedure = new AdmissionProcedure
            {
                StartApplyDocuments = DateTime.Today.AddDays(-10),
                EndApplyDocuments = DateTime.Today.AddDays(-1),
                RankingDate = DateTime.Today.Date,
                StartEnrollment = DateTime.Today.AddDays(1).Date,
                EndEnrollment = DateTime.Today.AddDays(8).Date,
                Status = AdmissionProcedureStatus.Finished,
                Year = DateTime.Now.Year,
                ParticipatedCandidates = new List<CandidateApplication>(),
            };

            await this.admissionProcedureRepository.AddAsync(admissionProcedure);
            await this.admissionProcedureRepository.SaveChangesAsync();

            var result = await this.StartAdmissionProcedure(admissionProcedure);

            return result;
        }

        public async Task<AdmissionProcedureServiceModel> GetLastProcedure()
        {
            var procedure = await this.admissionProcedureRepository.All()
                                    .OrderByDescending(d => d.Id)
                                    .FirstOrDefaultAsync();

            var model = new AdmissionProcedureServiceModel();

            if (procedure != null)
            {
                model.Status = procedure.Status;
                model.RankingDate = procedure.RankingDate.ToShortDateString();
                model.StartEnrollment = procedure.StartEnrollment.ToShortDateString();
                model.EndEnrollment = procedure.EndEnrollment.ToShortDateString();
            }

            return model;
        }

        public async Task<bool> RevertAdmissionProcedure()
        {
            var procedure = await this.admissionProcedureRepository.All()
                .LastOrDefaultAsync();
            procedure.Status = AdmissionProcedureStatus.Reverted;
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

            foreach (var school in schoolsFromDb)
            {
                var candidates = this.candidateApplicationRepository.All()
                                     .Include(c => c.Candidate)
                                     .Where(sc => sc.SchoolId == school.Id).ToList();

                int freeSpots = school.FreeSpots;

                if (candidates.Count != 0)
                {
                    foreach (var candidate in candidates)
                    {
                        if (candidate.Candidate.Status != CandidateStatus.Admitted && candidate.Candidate.IsDeleted == false)
                        {
                            var totalScores = this.calculatorService
                            .CalculateTotalScoreForTheAdmissionProcedure(candidate.CandidateId, school.Id);

                            candidate.TotalScoresForSchool = totalScores;
                            this.candidateApplicationRepository.Update(candidate);
                            await this.candidateApplicationRepository.SaveChangesAsync();
                        }
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
    }
}
