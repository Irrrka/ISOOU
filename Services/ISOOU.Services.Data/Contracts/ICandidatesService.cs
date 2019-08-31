namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ISOOU.Data.Models;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Users;

    public interface ICandidatesService
    {
        Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model);

        IQueryable<CandidateServiceModel> GetCandidates(ClaimsPrincipal userIdentity);

        Task<CandidateServiceModel> GetCandidateById(int id);

        Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel);

        IQueryable<CandidateServiceModel> GetCandidatesOfParent(ClaimsPrincipal userIdentity, int parentId);

        Task<bool> Delete(int id);

        Task<bool> AddApplications(int id, List<int> schoolApplicationIds);

        Task<bool> CreateDocument(CreateDocumentInputModel input);

        IEnumerable<int> GetCandidateApplications(int candidateId);

        Task<bool> UpdateRepository(int candidateId);
    }
}
