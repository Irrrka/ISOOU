namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;

    public interface IDistrictsService
    {

        Task<DistrictServiceModel> GetDistrictById(int id);

        IQueryable<DistrictServiceModel> GetAllDistrictsAsync();
    }
}
