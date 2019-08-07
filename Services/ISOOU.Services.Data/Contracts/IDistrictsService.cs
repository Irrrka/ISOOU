namespace ISOOU.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface IDistrictsService
    {
        Task<DistrictServiceModel> GetDistrictById(int id);

        Task<DistrictServiceModel> GetDistrictByName(string name);

        IQueryable<DistrictServiceModel> GetAllDistricts();
    }
}
