namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Users;

    public interface ICandidatesService
    {
        Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model);

        IQueryable<CandidateServiceModel> GetCandidates();

        Task<CandidateServiceModel> GetCandidateById(int id);

        Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel);

        Task<bool> Delete(int id);

        Task<bool> AddApplications(int id, List<int> schoolApplicationIds);

        Task<bool> CreateDocument(CreateDocumentInputModel input);
    }
}
