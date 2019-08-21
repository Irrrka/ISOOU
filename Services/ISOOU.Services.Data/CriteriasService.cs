using ISOOU.Data.Common.Repositories;
using ISOOU.Services.Data.Contracts;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Data.Models
{
    public class CriteriasService : ICriteriasService
    {
        private readonly IRepository<Criteria> criteriasRepository;
        private readonly IRepository<CriteriaForCandidate> criteriasForCandidatesRepository;

        public CriteriasService(
            IRepository<Criteria> criteriasRepository,
            IRepository<CriteriaForCandidate> criteriasForCandidatesRepositor)
        {
            this.criteriasRepository = criteriasRepository;
            this.criteriasForCandidatesRepository = criteriasForCandidatesRepositor;
        }

        public async Task<int> GetIdByCriteriaName(string criteriaName)
        {
            var id = (await this.criteriasRepository.All()
                .Where(n => n.Name.Contains(criteriaName)).FirstOrDefaultAsync()).Id;

            if (id == 0)
            {
                throw new ArgumentNullException(nameof(criteriaName));
            }

            return id;
        }

        public async Task<IEnumerable<CriteriaServiceModel>> GetAllCriterias()
        {
            var allcriterias = await this.criteriasRepository.All().To<CriteriaServiceModel>().ToListAsync();

            return allcriterias;
        }

        public async Task<IEnumerable<CriteriaForCandidateServiceModel>> GetCriteriasAndScoresByCandidateId(int candidateId)
        {
            var criterias = await this.criteriasForCandidatesRepository.All()
                .Where(c => c.CandidateId == candidateId)
                .To<CriteriaForCandidateServiceModel>()
                .Include(x => x.Candidate)
                .ThenInclude(y => y.Mother)
                .Include(x => x.Candidate)
                .ThenInclude(y => y.Father)
                .Include(x => x.Candidate)
                .ThenInclude(y => y.Applications)
                .Include(x => x.Criteria)
                .ToListAsync();

            return criterias;
        }

        public async Task<bool> DeleteCriteriasByCandidateId(int candidateId)
        {
            var criterias = await this.criteriasForCandidatesRepository.All()
                .Where(c => c.CandidateId == candidateId).ToListAsync();

            var result = 0;

            foreach (var criteria in criterias)
            {
                this.criteriasForCandidatesRepository.Delete(criteria);
                result = await this.criteriasForCandidatesRepository.SaveChangesAsync();
            }

            return result>0;
        }
    }
}
