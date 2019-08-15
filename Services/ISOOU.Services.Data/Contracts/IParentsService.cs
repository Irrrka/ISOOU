namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Models;

    public interface IParentsService
    {
        Task<bool> Create(ClaimsPrincipal userIdentity, ParentServiceModel parentServiceModel);

        IQueryable<ParentServiceModel> GetParents(ClaimsPrincipal userIdentity);

        Task<string> GetParentFullNameByRole(ClaimsPrincipal userIdentity, ParentRole role);

        Task<int> GetParentIdByFullName(ClaimsPrincipal userIdentity, string fullName);

        Task<ParentServiceModel> GetParentById(int id);

        Task<bool> Edit(int id, ParentServiceModel parentServiceModel);

        Task<bool> Delete(int id);
    }
}
