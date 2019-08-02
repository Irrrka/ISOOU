namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface ICandidatesService
    {
        Task<bool> Create(CandidateServiceModel model);

        Task<IQueryable<CandidateServiceModel>> GetCandidates();

        Task<CandidateServiceModel> GetCandidateById(int id);

        Task<bool> Edit(CandidateServiceModel candidateServiceModel);

        Task<bool> Delete(int id);

        Task<Dictionary<string, int>> CalculateScoresByCriteria(int id);
    }
}
