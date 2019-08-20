namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CandidatesService : ICandidatesService
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IParentsService parentsService;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<CandidateApplication> candidateApplicationsRepository;
        private readonly IRepository<Criteria> criteriasRepository;
        private readonly IRepository<CandidateApplication> schoolCandidateRepository;
        private readonly IRepository<Document> documentRepository;
        private readonly ISchoolsService schoolService;
        private readonly ICalculatorService calculatorService;
        private readonly ICloudinaryService claudinaryService;

        public CandidatesService(
            UserManager<SystemUser> userManager,
            IRepository<Candidate> candidatesRepository,
            IRepository<CandidateApplication> candidateApplicationsRepository,
            IParentsService parentsService,
            IRepository<Criteria> criteriasRepository,
            IRepository<CandidateApplication> schoolCandidateRepository,
            ISchoolsService schoolService,
            ICalculatorService calculatorService,
            ICloudinaryService claudinaryService,
            IRepository<Document> documentRepository)
        {
            this.userManager = userManager;
            this.candidatesRepository = candidatesRepository;
            this.candidateApplicationsRepository = candidateApplicationsRepository;
            this.parentsService = parentsService;
            this.criteriasRepository = criteriasRepository;
            this.schoolCandidateRepository = schoolCandidateRepository;
            this.schoolService = schoolService;
            this.calculatorService = calculatorService;
            this.claudinaryService = claudinaryService;
            this.documentRepository = documentRepository;
        }

        public async Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var userId = this.userManager.GetUserId(userIdentity);

            Candidate candidate = new Candidate
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                UCN = model.UCN,
                YearOfBirth = model.YearOfBirth,
                UserId = userId,
                MotherId = model.MotherId,
                FatherId = model.FatherId,
                Desease = model.Desease,
                SEN = model.SEN,
                Immunization = model.Immunization,
                KinderGarten = model.KinderGarten,
            };

            await this.candidatesRepository.AddAsync(candidate);
            var result = await this.candidatesRepository.SaveChangesAsync();

            int candidateId = candidate.Id;
            int basicScores = await this.calculatorService.CalculateBasicScoresByCriteria(candidateId);
            candidate.BasicScores = basicScores;

            this.candidatesRepository.Update(candidate);
            result = await this.candidatesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<CandidateServiceModel> GetCandidateById(int id)
        {
            var candidate = await this.candidatesRepository
                               .All()
                               .To<CandidateServiceModel>()
                               .Include(x => x.Mother)
                               .ThenInclude(y => y.Address)
                               .Include(x => x.Father)
                               .ThenInclude(y => y.Address)
                               .Include(z => z.Applications)
                               .Include(a => a.BasicScores)
                               .Include(b => b.Criterias)
                               .SingleOrDefaultAsync(p => p.Id == id);
            return candidate;
        }

        public IQueryable<CandidateServiceModel> GetCandidates()
        {
            var candidates = this.candidatesRepository
                .All()
                .To<CandidateServiceModel>();

            return candidates;
        }

        public async Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel)
        {

            Candidate candidateToEdit = await this.candidatesRepository.All()
                                                    .FirstOrDefaultAsync(p => p.Id == id);
            if (candidateToEdit == null)
            {
                throw new ArgumentNullException();
            }

            var userId = this.userManager.GetUserId(userIdentity);

            candidateToEdit.UserId = userId;
            candidateToEdit.UCN = candidateToEdit.UCN;
            candidateToEdit.YearOfBirth = candidateToEdit.YearOfBirth;

            candidateToEdit.Desease = candidateServiceModel.Desease;
            candidateToEdit.SEN = candidateServiceModel.SEN;
            candidateToEdit.Immunization = candidateServiceModel.Immunization;
            candidateToEdit.KinderGarten = candidateServiceModel.KinderGarten;
            candidateToEdit.FirstName = candidateServiceModel.FirstName;
            candidateToEdit.MiddleName = candidateServiceModel.MiddleName;
            candidateToEdit.LastName = candidateServiceModel.LastName;

            candidateToEdit.FatherId = candidateServiceModel.FatherId;
            candidateToEdit.MotherId = candidateServiceModel.MotherId;

            this.candidatesRepository.Update(candidateToEdit);
            var result = await this.candidatesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var candidateToDelete = await this.candidatesRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == id);

            if (candidateToDelete == null)
            {
                throw new ArgumentNullException();
            }

            candidateToDelete.IsDeleted = true;
            this.candidatesRepository.Update(candidateToDelete);
            var result = await this.candidatesRepository.SaveChangesAsync();

            return result>0;
        }

        public async Task<bool> AddApplications(int id, List<int> schoolApplicationIds)
        {
            var candidateFomDb = await this.candidatesRepository
                               .All()
                               .FirstOrDefaultAsync(p => p.Id == id);

            if (candidateFomDb == null)
            {
                throw new ArgumentNullException();
            }

            var result = 0;
            //When edit the application TODO!!!
            List<CandidateApplication> apps = this.candidateApplicationsRepository.All().Where(c => c.CandidateId == candidateFomDb.Id).ToList();

            foreach (var app in apps)
            {
                this.candidateApplicationsRepository.Delete(app);
                await this.candidatesRepository.SaveChangesAsync();
            }

            for (int i = 0; i < schoolApplicationIds.Count; i++)
            {
                candidateFomDb.Applications.Add(
                new CandidateApplication
                    {
                        CandidateId = candidateFomDb.Id,
                        SchoolId = schoolApplicationIds[i],
                    });

                this.candidatesRepository.Update(candidateFomDb);
                result = await this.candidatesRepository.SaveChangesAsync();


                candidateFomDb
                    .Applications
                    .FirstOrDefault(sc => sc.SchoolId == schoolApplicationIds[i])
                    .AdditionalScoresForSchool = this.calculatorService.CalculateAdditionalScoresForNumberOfWish(i + 1)
                                          + (await this.calculatorService.CalculateAdditionalScoresForSchools(id, schoolApplicationIds[i]));

                this.candidatesRepository.Update(candidateFomDb);
                result = await this.candidatesRepository.SaveChangesAsync();
            }

            return result > 0;
        }

        public async Task<bool> CreateDocumentSubmission(CreateDocumentInputModel input)
        {
            var file = input.Application;
            var url = await this.claudinaryService.UploadDocument(file, Guid.NewGuid().ToString());
            var candidate = this.candidateApplicationsRepository.All()
                .FirstOrDefault(c => c.CandidateId == input.CandidateId);
            var candidateId = candidate.CandidateId;
            var schoolId = candidate.SchoolId;

            var document = new Document
            {
                Name = file.ToString(),
                Url = url,
                SchoolId = schoolId,
                CandidateId = candidateId,
            };

            await this.documentRepository.AddAsync(document);
            var result = await this.documentRepository.SaveChangesAsync();

            return result>0;
        }
    }
}
