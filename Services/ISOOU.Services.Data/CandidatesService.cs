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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CandidatesService : ICandidatesService
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IParentsService parentsService;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<Criteria> criteriasRepository;
        private readonly IRepository<SchoolCandidate> schoolCandidateRepository;
        private readonly ISchoolsService schoolService;

        public CandidatesService(
            UserManager<SystemUser> userManager,
            IRepository<Candidate> candidatesRepository,
            IParentsService parentsService,
            IRepository<Criteria> criteriasRepository,
            IRepository<SchoolCandidate> schoolCandidateRepository,
            ISchoolsService schoolService)
        {
            this.userManager = userManager;
            this.candidatesRepository = candidatesRepository;
            this.parentsService = parentsService;
            this.criteriasRepository = criteriasRepository;
            this.schoolCandidateRepository = schoolCandidateRepository;
            this.schoolService = schoolService;
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
                               .Include(z => z.SchoolCandidates)
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

            //Parent mother = (await this.candidatesRepository.All()
                                                  //  .FirstOrDefaultAsync(p => p.MotherId == candidateServiceModel.MotherId))
                                                  //  .Mother;
           // Parent father = (await this.candidatesRepository.All()
                                                //   .FirstOrDefaultAsync(p => p.FatherId == candidateServiceModel.FatherId))
                                                //   .Father;
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
            await this.candidatesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<int>> CalculateAdditionalScoresByPositionOfApplication(int id)
        {
            var scoresForApplications = new List<int>();

            var candidate = await this.candidatesRepository
                              .All()
                              .SingleOrDefaultAsync(p => p.Id == id);

            //TODO not working properly anymore
            int scores = candidate.Criterias.Sum(x => x.Criteria.Scores);

            //TODO Scalability?!
            int scoresFirstApplication = scores + GlobalConstants.FirstApplicationBonusCriteria;
            scoresForApplications.Add(scoresFirstApplication);

            int scoresSecondApplication = scores + GlobalConstants.SecondApplicationBonusCriteria;
            scoresForApplications.Add(scoresSecondApplication);

            int scoresThirdApplication = scores + GlobalConstants.ThirdApplicationBonusCriteria;
            scoresForApplications.Add(scoresThirdApplication);

            return scoresForApplications;
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

            for (int i = 0; i < schoolApplicationIds.Count; i++)
            {
                candidateFomDb.SchoolCandidates.Add(
                    new SchoolCandidate
                    {
                        Id = i + 1,
                        CandidateId = candidateFomDb.Id,
                        SchoolId = schoolApplicationIds[i],
                    });
            }

            this.candidatesRepository.Update(candidateFomDb);
            result = await this.candidatesRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
