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
        private readonly IRepository<SystemUser> usersRepository;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<Parent> parentsRepository;
        private readonly IRepository<Criteria> criteriasRepository;

        public CandidatesService(
            IRepository<SystemUser> usersRepository,
            IRepository<Candidate> candidatesRepository,
            IRepository<Parent> parentsRepository,
            IRepository<Criteria> criteriasRepository)
        {
            this.usersRepository = usersRepository;
            this.candidatesRepository = candidatesRepository;
            this.parentsRepository = parentsRepository;
            this.criteriasRepository = criteriasRepository;
        }

        public async Task<bool> Create(string userIdentity, CandidateServiceModel model)
        {
            if (model==null)
            {
                throw new ArgumentNullException();
            }

            SystemUser user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.UserName == userIdentity);

            var candidate = model.To<Candidate>();
            candidate.User = user;

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
                .Include(x => x.User)
                .To<CandidateServiceModel>();

            return candidates;
        }

        public async Task<bool> Edit(string userIdentity, CandidateServiceModel candidateServiceModel)
        {
            var candidateToEdit = await this.candidatesRepository
                                .All()
                                .FirstOrDefaultAsync(p => p.Id == candidateServiceModel.Id);
            if (candidateToEdit == null)
            {
                throw new ArgumentNullException();
            }

            SystemUser user = await this.usersRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.UserName == userIdentity);

           
            candidateToEdit.FirstName = candidateServiceModel.FirstName;
            candidateToEdit.MiddleName = candidateServiceModel.MiddleName;
            candidateToEdit.LastName = candidateServiceModel.LastName;
            candidateToEdit.SEN = candidateServiceModel.SEN;
            candidateToEdit.Desease = candidateServiceModel.Desease;
            candidateToEdit.User = user;
            candidateToEdit.KinderGarten = candidateToEdit.KinderGarten;
            candidateToEdit.YearOfBirth = candidateToEdit.YearOfBirth;
            candidateToEdit.UCN = candidateToEdit.UCN;

            //candidateToEdit.Mother = await this.parentsRepository
            //                    .All()
            //                    .SingleOrDefaultAsync(p => p.Id == candidateServiceModel.Mother.Id);
            //candidateToEdit.Father = await this.parentsRepository
            //                    .All()
            //                    .SingleOrDefaultAsync(p => p.Id == candidateServiceModel.Father.Id);

            this.candidatesRepository.Update(candidateToEdit);
            var result = await this.candidatesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var candidateToDelete = await this.candidatesRepository
                               .All()
                               .SingleOrDefaultAsync(p => p.Id == id);
            CoreValidator.EnsureNotNull(GlobalConstants.CandidateNotFound);

            candidateToDelete.IsDeleted = true;
            this.candidatesRepository.Update(candidateToDelete);
            await this.candidatesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<Dictionary<string, int>> CalculateScoresByCriteria(int id)
        {
            var scoresByCriteria = new Dictionary<string, int>();

            var candidate = await this.candidatesRepository
                              .All()
                              .SingleOrDefaultAsync(p => p.Id == id);
            var mother = candidate.Mother;
            var father = candidate.Father;

            //TODO additionalScoresBySchool
            var schoolsClasses = candidate
                                    .CandidateSchoolClasses.Select(c => c.SchoolClass);

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

            foreach (var schoolclass in schoolsClasses)
            {
                CalculateParentDistrictCriteria(scoresByCriteria, mother, father, schoolclass);

                CalculateParentWorkDistrictCriteria(scoresByCriteria, mother, father, schoolclass);

                //this.ScoresByCriteriaByCandidates[candidate][nameof(additionalScoresBySchool)]= additionalScoresBySchool[schoolclass.School];

                foreach (var kvp in scoresByCriteria)
                {
                    var criteria = kvp.Key;
                    var scores = kvp.Value;
                    var criteriaForDB = new Criteria()
                    {
                        Name = criteria,
                        Scores = scores,
                        CandidateId = candidate.Id,
                        Candidate = candidate,
                    };

                    candidate.Scores.Add(criteriaForDB);
                    await this.criteriasRepository.AddAsync(criteriaForDB);
                    await this.criteriasRepository.SaveChangesAsync();
                    //TODO Map by Id?
                }

                this.candidatesRepository.Update(candidate);
                await this.candidatesRepository.SaveChangesAsync();

            }
            //Todo CalculationOfChangingSchool????
            return scoresByCriteria;
        }

        private static void CalculateParentWorkDistrictCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father, SchoolClass schoolclass)
        {
            if (mother.WorkDistrict == schoolclass.School.District
                || father.WorkDistrict == schoolclass.School.District)
            {
                var parentWorksInDistrict = GlobalConstants.ParentHasWorkInDistrictCriteria;

                if (scoresByCriteria.ContainsKey(nameof(parentWorksInDistrict)))
                {
                    scoresByCriteria.Add(nameof(parentWorksInDistrict), 0);
                }

                scoresByCriteria[nameof(parentWorksInDistrict)] = parentWorksInDistrict;

                // additionalScoresBySchool[schoolclass.School] += this.ParentWorksInDistrict;
            }
        }

        private static void CalculateParentDistrictCriteria(Dictionary<string, int> scoresByCriteria, Parent mother, Parent father, SchoolClass schoolclass)
        {
            if (mother.Address.CurrentDistrict == schoolclass.School.District
                                || father.Address.CurrentDistrict == schoolclass.School.District)
            {
                var parentAddressDistrict = GlobalConstants.ParentCurrentDistrictCriteria;

                if (!scoresByCriteria.ContainsKey(nameof(parentAddressDistrict)))
                {
                    scoresByCriteria.Add(nameof(parentAddressDistrict), 0);
                }

                scoresByCriteria[nameof(parentAddressDistrict)] = parentAddressDistrict;
            }

            if (mother.Address.PermanentDistrict == schoolclass.School.District
                || father.Address.PermanentDistrict == schoolclass.School.District)
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
    }
}
