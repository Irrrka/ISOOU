﻿namespace ISOOU.Services.Data
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

        public async Task<Dictionary<string, int>> CalculateScoresByCriteria(int id, SchoolServiceModel school)
        {
            var scoresByCriteria = new Dictionary<string, int>();

            var candidate = await this.candidatesRepository
                              .All()
                              .SingleOrDefaultAsync(p => p.Id == id);

            Parent mother = candidate.MotherId.To<Parent>();
            Parent father = candidate.FatherId.To<Parent>();

            CalculateCityCriteria(scoresByCriteria, mother, father);

            CalculateParentWorksCriteria(scoresByCriteria, mother, father);

            CalculateHasNoParentCriteria(scoresByCriteria, mother, father);

            CalculateHasVisitKGCriteria(scoresByCriteria, candidate);

            //TODO Check?
            CalculateHasManyBrosCriteria(scoresByCriteria, mother, father);

            CalculateHasSENCriteria(scoresByCriteria, candidate);

            CalculateHasDeseasCriteria(scoresByCriteria, candidate);

            //todoo add addirional scores
            var additionalScoresBySchool = new Dictionary<SchoolServiceModel, int>();
            scoresByCriteria.Add(nameof(additionalScoresBySchool), 0);

            //foreach (var schoolclass in schoolsClasses)
            //{
            //    CalculateParentDistrictCriteria(scoresByCriteria, mother, father, schoolclass);

            //    CalculateParentWorkDistrictCriteria(scoresByCriteria, mother, father, schoolclass);

            //    //this.ScoresByCriteriaByCandidates[candidate][nameof(additionalScoresBySchool)]= additionalScoresBySchool[schoolclass.School];

            //    foreach (var kvp in scoresByCriteria)
            //    {
            //        var criteria = kvp.Key;
            //        var scores = kvp.Value;
            //        var criteriaForDB = new Criteria()
            //        {
            //            Name = criteria,
            //            Scores = scores,
            //            CandidateId = candidate.Id,
            //            Candidate = candidate,
            //        };

            //        candidate.Scores.Add(criteriaForDB);
            //        await this.criteriasRepository.AddAsync(criteriaForDB);
            //        await this.criteriasRepository.SaveChangesAsync();
            //        //TODO Map by Id?
            //    }

            //    this.candidatesRepository.Update(candidate);
            //    await this.candidatesRepository.SaveChangesAsync();

            //}
            //Todo CalculationOfChangingSchool????
            return scoresByCriteria;
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

        public async Task<bool> AddApplications(int candidateId, ClaimsPrincipal userIdentity, List<SchoolCandidateServiceModel> applicationsToAdd)
        {
            

            var candidateFomDb = await this.candidatesRepository
                               .All()
                               .FirstOrDefaultAsync(p => p.Id == candidateId);

            if (candidateFomDb == null)
            {
                throw new ArgumentNullException();
            }

            var result = 0;

            foreach (var applicationToAdd in applicationsToAdd)
            {
                var schoolFromDb = this.schoolService
                    .GetSchoolDetailsById(applicationToAdd.SchoolId)
                    .To<School>();

                var schoolCandidate = new SchoolCandidate
                                        {
                                            Candidate = candidateFomDb,
                                            CandidateId = applicationToAdd.CandidateId,
                                            School = schoolFromDb,
                                            SchoolId = applicationToAdd.SchoolId,
                                        };

                this.schoolCandidateRepository.Update(schoolCandidate);
                result = await this.schoolCandidateRepository.SaveChangesAsync();
            }

            return result > 0;
        }




        private static void CalculateParentWorkDistrictCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father, School school)
        {
            if (mother.WorkDistrict.Name == school.District.Name
                || father.WorkDistrict.Name == school.District.Name)
            {
                var parentWorksInDistrict = GlobalConstants.ParentHasWorkInDistrictCriteria;

                if (scoresByCriteria.ContainsKey(nameof(parentWorksInDistrict)))
                {
                    scoresByCriteria.Add(nameof(parentWorksInDistrict), 0);
                }

                scoresByCriteria[nameof(parentWorksInDistrict)] = parentWorksInDistrict;

                //additionalScoresBySchool[schoolclass.School] += this.ParentWorksInDistrict;
            }
        }

        private static void CalculateParentDistrictCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father, School school)
        {
            if (mother.Address.CurrentDistrict.Name == school.District.Name
                                || father.Address.CurrentDistrict.Name == school.District.Name)
            {
                var parentAddressDistrict = GlobalConstants.ParentCurrentDistrictCriteria;

                if (!scoresByCriteria.ContainsKey(nameof(parentAddressDistrict)))
                {
                    scoresByCriteria.Add(nameof(parentAddressDistrict), 0);
                }

                scoresByCriteria[nameof(parentAddressDistrict)] = parentAddressDistrict;
            }

            if (mother.Address.PermanentDistrict == school.District
                || father.Address.PermanentDistrict == school.District)
            {
                var parentAddressDistrict = GlobalConstants.ParentPermanentDistrictCriteria;

                if (!scoresByCriteria.ContainsKey(nameof(parentAddressDistrict)))
                {
                    scoresByCriteria.Add(nameof(parentAddressDistrict), 0);
                }

                scoresByCriteria[nameof(parentAddressDistrict)] = parentAddressDistrict;
            }
        }

        private static void CalculateHasDeseasCriteria(Dictionary<string, int> scoresByCriteria, Candidate candidate)
        {
            if (candidate.Desease)
            {
                var hasDeseas = GlobalConstants.HasDeseasCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(hasDeseas)))
                {
                    scoresByCriteria.Add(nameof(hasDeseas), 0);
                }

                scoresByCriteria[nameof(hasDeseas)] = hasDeseas;
            }
        }

        private static void CalculateHasSENCriteria(Dictionary<string, int> scoresByCriteria, Candidate candidate)
        {
            if (candidate.SEN)
            {
                var sen = GlobalConstants.HasSENCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(sen)))
                {
                    scoresByCriteria.Add(nameof(sen), 0);
                }

                scoresByCriteria[nameof(sen)] = sen;
            }
        }

        private static void CalculateHasManyBrosCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father)
        {
            if (mother.Candidates.Count >= GlobalConstants.ChildrenInFamily || father.Candidates.Count >= GlobalConstants.ChildrenInFamily)
            {
                var hasManyBrothersOrSisters = GlobalConstants.HasManyBrothersOrSistersCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(hasManyBrothersOrSisters)))
                {
                    scoresByCriteria.Add(nameof(hasManyBrothersOrSisters), 0);
                }

                scoresByCriteria[nameof(hasManyBrothersOrSisters)] = hasManyBrothersOrSisters;
            }
        }

        private static void CalculateHasVisitKGCriteria(Dictionary<string, int> scoresByCriteria, Candidate candidate)
        {
            if (candidate.KinderGarten != null)
            {
                var hasVisitKG = GlobalConstants.HasVisitKGCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(hasVisitKG)))
                {
                    scoresByCriteria.Add(nameof(hasVisitKG), 0);
                }

                scoresByCriteria[nameof(hasVisitKG)] = hasVisitKG;
            }
        }

        private static void CalculateHasNoParentCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father)
        {
            if (mother == null && father == null)
            {
                var hasNoParent = GlobalConstants.HasNoParentCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(hasNoParent)))
                {
                    scoresByCriteria.Add(nameof(hasNoParent), 0);
                }

                scoresByCriteria[nameof(hasNoParent)] = hasNoParent;
            }

            if (mother == null || father == null)
            {
                var hasOneParent = GlobalConstants.HasOneParentCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(hasOneParent)))
                {
                    scoresByCriteria.Add(nameof(hasOneParent), 0);
                }

                scoresByCriteria[nameof(hasOneParent)] = hasOneParent;
            }
        }

        private static void CalculateParentWorksCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father)
        {
            if (mother.WorkName != null)
            {
                var motherHasWork = GlobalConstants.ParentHasWorkCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(motherHasWork)))
                {
                    scoresByCriteria.Add(nameof(motherHasWork), 0);
                }

                scoresByCriteria[nameof(motherHasWork)] = motherHasWork;
            }

            if (father.WorkName != null)
            {
                var fatherHasWork = GlobalConstants.ParentHasWorkCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(fatherHasWork)))
                {
                    scoresByCriteria.Add(nameof(fatherHasWork), 0);
                }

                scoresByCriteria[nameof(fatherHasWork)] = fatherHasWork;
            }
        }

        private static void CalculateCityCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father)
        {
            if (mother.Address.CurrentCity == CityName.София
                            || father.Address.CurrentCity == CityName.София)
            {
                var parentAddressCity = GlobalConstants.ParentCurrentCitySofiaCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(parentAddressCity)))
                {
                    scoresByCriteria.Add(nameof(parentAddressCity), 0);
                }

                scoresByCriteria[nameof(parentAddressCity)] = parentAddressCity;
            }

            if (mother.Address.PermanentCity == CityName.София
                || father.Address.PermanentCity == CityName.София)
            {
                var parentAddressCity = GlobalConstants.ParentPermanentCitySofiaCriteria;
                if (!scoresByCriteria.ContainsKey(nameof(parentAddressCity)))
                {
                    scoresByCriteria.Add(nameof(parentAddressCity), 0);
                }

                scoresByCriteria[nameof(parentAddressCity)] = parentAddressCity;
            }
        }

        public Task<bool> Create(string userIdentity, CandidateServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
