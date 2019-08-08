namespace ISOOU.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface ISchoolsService
    {
        Task<IQueryable<SchoolServiceModel>> GetAllSchoolsByDistrictId(int id);

        IQueryable<SchoolServiceModel> GetAllSchools();

        Task<SchoolServiceModel> GetSchoolDetailsById(int id);

        Task<SchoolServiceModel> GetSchoolDetailsByName(string name);

        Task<bool> EditSchool(SchoolServiceModel model);

        Task<SchoolServiceModel> GetSchoolForEdit(string userIdentity);
    }
}
