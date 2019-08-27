namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CalculatorService : ICalculatorService
    {
        private readonly IParentsService parentsService;
        private readonly IRepository<Candidate> candidatesRepository;
        private readonly IRepository<CriteriaForCandidate> criteriaForCandidatesRepository;
        private readonly ICriteriasService criteriasService;
        private readonly IRepository<CandidateApplication> candidateApplicationRepository;

        public CalculatorService(
            IRepository<Candidate> candidatesRepository,
            IRepository<CandidateApplication> candidateApplicationRepository,
            IParentsService parentsService,
            IRepository<CriteriaForCandidate> criteriaForCandidatesRepository,
            ICriteriasService criteriasService)
        {
            this.candidatesRepository = candidatesRepository;
            this.parentsService = parentsService;
            this.criteriasService = criteriasService;
            this.criteriaForCandidatesRepository = criteriaForCandidatesRepository;
            this.candidateApplicationRepository = candidateApplicationRepository;
        }

        public async Task<int> CalculateBasicScoresByCriteria(int candidateId)
        {
            var candidate = await this.candidatesRepository
                               .All()
                               .Include(c => c.Criterias)
                               .Include(m => m.Mother)
                               .ThenInclude(a => a.Address)
                               .Include(f => f.Father)
                               .ThenInclude(a => a.Address)
                               .FirstOrDefaultAsync(p => p.Id == candidateId);
            var fatherFullName = candidate.Father.FullName.TrimEnd();
            var motherFullName = candidate.Mother.FullName.TrimEnd();
            var fatherId = candidate.FatherId;
            var motherId = candidate.MotherId;

            var brothersOrSisters = await this.candidatesRepository.All()
                                                     .Where(p => p.FatherId == fatherId || p.MotherId == motherId)
                                                     .ToListAsync();

            if (candidate == null)
            {
                throw new ArgumentNullException();
            }

            if (candidate.Immunization == true)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasAllTheImmunizations));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasAllTheImmunizations),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    // SchId = 0,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.Desease == true)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasDeseasCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasDeseasCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    //  SchId = 0,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.SEN == true)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasSENCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasSENCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    //  SchId = 0,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.KinderGarten != null)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasVisitKGCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasVisitKGCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    //  SchId = 0,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if ((motherFullName == ParentRole.Няма.ToString() && fatherFullName == ParentRole.Няма.ToString())
                || (motherFullName == ParentRole.Друг.ToString() && fatherFullName == ParentRole.Друг.ToString()))
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasNoParentCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasNoParentCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }
            else if ((motherFullName == ParentRole.Няма.ToString() || fatherFullName == ParentRole.Няма.ToString())
            || (motherFullName == ParentRole.Друг.ToString() || fatherFullName == ParentRole.Друг.ToString()))
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasOneParentCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasOneParentCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.Mother.WorkName != null)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.MotherHasWorkCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.MotherHasWorkCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.Father.WorkName != null)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.FatherHasWorkCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.FatherHasWorkCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (candidate.Mother.Address.PermanentCity == CityName.София || candidate.Father.Address.PermanentCity == CityName.София)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.ParentPermanentCitySofiaCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.ParentPermanentCitySofiaCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }
            else if (candidate.Mother.Address.CurrentCity == CityName.София || candidate.Father.Address.CurrentCity == CityName.София)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.ParentCurrentCitySofiaCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.ParentCurrentCitySofiaCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            if (brothersOrSisters.Count >= GlobalConstants.ChildrenInFamily)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.HasManyBrothersOrSistersCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Name = nameof(GlobalConstants.HasManyBrothersOrSistersCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.candidatesRepository.SaveChangesAsync();
            }

            var basicScores = candidate.Criterias.Sum(x => x.Criteria.Scores);
            return basicScores;
        }

        public async Task<int> CalculateAdditionalScoresForSchools(int candidateId, int schoolId)
        {
            var candidate = await this.candidatesRepository
                               .All()
                               .Include(a => a.Applications)
                               .Include(c => c.Criterias)
                               .Include(m => m.Mother)
                               .ThenInclude(a => a.Address)
                               .Include(f => f.Father)
                               .ThenInclude(a => a.Address)
                               .FirstOrDefaultAsync(p => p.Id == candidateId);

            if (candidate == null)
            {
                throw new ArgumentNullException();
            }

            var criteriaScores = 0;

            var schoolDistrictId = candidate.Applications.FirstOrDefault(sc => sc.SchoolId == schoolId).School.DistrictId;
            var motherPermanentDistrictId = candidate.Mother.Address.PermanentDistrictId;
            var fatherPermanentDistrictId = candidate.Father.Address.PermanentDistrictId;
            var motherCurrenttDistrictId = candidate.Mother.Address.CurrentDistrictId;
            var fatherCurrenttDistrictId = candidate.Father.Address.CurrentDistrictId;
            var motherWorkDistrictId = candidate.Mother.WorkDistrictId;
            var fatherWorkDistrictId = candidate.Father.WorkDistrictId;

            if (schoolDistrictId == motherPermanentDistrictId || schoolDistrictId == fatherPermanentDistrictId)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.ParentPermanentDistrictSchoolCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Sch = schoolId,
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    Name = nameof(GlobalConstants.ParentPermanentDistrictSchoolCriteria),
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.criteriaForCandidatesRepository.SaveChangesAsync();

                criteriaScores += candidate.Criterias.FirstOrDefault(x => x.CriteriaId == criteriaId).Criteria.Scores;
            }
            else if (schoolDistrictId == motherCurrenttDistrictId || schoolDistrictId == motherCurrenttDistrictId)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.ParentCurrentDistrictSchoolCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Sch = schoolId,
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                    Name = nameof(GlobalConstants.ParentCurrentDistrictSchoolCriteria),
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.criteriaForCandidatesRepository.SaveChangesAsync();

                criteriaScores += candidate.Criterias.FirstOrDefault(c => c.CriteriaId == criteriaId).Criteria.Scores;
            }

            if (schoolDistrictId == motherWorkDistrictId || schoolDistrictId == fatherWorkDistrictId)
            {
                var criteriaId = await this.criteriasService
                    .GetIdByCriteriaName(nameof(GlobalConstants.ParentHasWorkInDistrictSchoolCriteria));
                var criteriaForCandidate = new CriteriaForCandidate
                {
                    Sch = schoolId,
                    Name = nameof(GlobalConstants.ParentHasWorkInDistrictSchoolCriteria),
                    CandidateId = candidateId,
                    CriteriaId = criteriaId,
                };
                await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
                await this.criteriaForCandidatesRepository.SaveChangesAsync();

                criteriaScores += candidate.Criterias.FirstOrDefault(c => c.CriteriaId == criteriaId).Criteria.Scores;
            }

            return criteriaScores;
        }

        public int CalculateAdditionalScoresForNumberOfWish(int numOfWish)
        {
            int result = 0;

            switch (numOfWish)
            {
                case 1:
                    result = GlobalConstants.FirstWishApplicationSchoolCriteria;
                    break;
                case 2:
                    result = GlobalConstants.SecondWishApplicationSchoolCriteria;
                    break;
                case 3:
                    result = GlobalConstants.ThirdWishApplicationSchoolCriteria;
                    break;
                default:
                    break;
            }

            return result;
        }

        public int CalculateTotalScoreForTheAdmissionProcedure(int candidateId, int schoolId)
        {
            var candidate = this.candidateApplicationRepository.All()
                .Where(id => id.CandidateId == candidateId
                            && id.SchoolId == schoolId)
                .Include(c => c.Candidate)
                .Include(s => s.School)
                .FirstOrDefault();

            var totalScores = candidate.AdditionalScoresForSchool + candidate.Candidate.BasicScores;

            return totalScores;
        }

        public async Task<bool> EditBasicScoresForManyBrothersAndSisters(int candidateId)
        {
            //var candidate = await this.candidatesRepository
            //                   .All()
            //                   .Include(c => c.Criterias)
            //                   .FirstOrDefaultAsync(p => p.Id == candidateId);

            var criteriaId = await this.criteriasService
                .GetIdByCriteriaName(nameof(GlobalConstants.HasManyBrothersOrSistersCriteria));
            //var candCrit = this.criteriaForCandidatesRepository.All()
            //    .Where(c => c.CandidateId == candidate.Id).ToList();

            //if (candCrit.Any(c => c.CriteriaId == criteriaId))
            //{
            //    return candidate.BasicScores;
            //}

            var criteriaForCandidate = new CriteriaForCandidate
            {
                Name = nameof(GlobalConstants.HasManyBrothersOrSistersCriteria),
                CandidateId = candidateId,
                CriteriaId = criteriaId,
            };
            await this.criteriaForCandidatesRepository.AddAsync(criteriaForCandidate);
            var result = await this.candidatesRepository.SaveChangesAsync();

            //var basicScores = candidate.Criterias.Sum(x => x.Criteria.Scores);
            return result > 0;
        }

        public int CalculateCoeficientByYear(int yearOfBirth)
        {
            var years = this.GetAllPossibleYearsToApply();

            Random random = new Random();

            Dictionary<int, int> coefByYear = new Dictionary<int, int>();

            foreach (var year in years)
            {
                var coef = year == DateTime.Now.Year - GlobalConstants.InTimeCandidate
                                ? GlobalConstants.MaxCoefOfAdmissionYear
                                : GlobalConstants.MinCoefOfAdmissionYear;
                coefByYear
                    .Add(year, coef);
            }

            return coefByYear[yearOfBirth];
        }

        public IEnumerable<int> GetAllPossibleYearsToApply()
        {
            int initialYear = DateTime.Now.Year;
            int youngestCandidatePossibleYear = initialYear - GlobalConstants.YoungestCandidate;
            int oldestCandidatePossibleYear = initialYear - GlobalConstants.OldestCandidate;

            var possibleYears = new List<int>();
            for (int year = youngestCandidatePossibleYear; year >= oldestCandidatePossibleYear; year--)
            {
                possibleYears.Add(year);
            }

            return possibleYears;
        }

    }
}
