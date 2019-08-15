namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface ICandidatesService
    {
        Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model);

        IQueryable<CandidateServiceModel> GetCandidates();

        Task<CandidateServiceModel> GetCandidateById(int id);

        Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel);

        Task<bool> Delete(int id);

        Task<Dictionary<string, int>> CalculateScoresByCriteria(int id, SchoolServiceModel school);

        Task<List<int>> CalculateAdditionalScoresByPositionOfApplication(int id);

        Task<bool> AddApplications(int candidateId, ClaimsPrincipal userIdentity, List<SchoolCandidateServiceModel> applicationsToAdd);
    }
}
