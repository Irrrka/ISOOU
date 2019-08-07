namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface IParentsService
    {
        Task<bool> Create(string userIdentity, ParentServiceModel parentServiceModel);

        IQueryable<ParentServiceModel> GetParents();

        Task<ParentServiceModel> GetParentById(int id);

        Task<bool> Edit(string userIdentity, ParentServiceModel parentServiceModel);

        Task<bool> Delete(int id);
    }
}
