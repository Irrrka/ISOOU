namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface IParentsService
    {
        Task<bool> Create(ParentServiceModel parentServiceModel);

        Task<IQueryable<ParentServiceModel>> GetParents();

        Task<ParentServiceModel> GetParentById(int id);

        Task<bool> Edit(ParentServiceModel parentServiceModel);

        Task<bool> Delete(int id);
    }
}
