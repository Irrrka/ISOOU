namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICriteriasService
    {
        Task<int> GetIdByCriteriaName(string criteriaName);

        Task<IEnumerable<CriteriaServiceModel>> GetAllCriterias();

        Task<IEnumerable<CriteriaForCandidateServiceModel>> GetCriteriasAndScoresByCandidateId(int candidateId);

        Task<bool> DeleteCriteriasByCandidateId(int candidateId);
    }
}
