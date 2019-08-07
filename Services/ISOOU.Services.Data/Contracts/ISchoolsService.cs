namespace ISOOU.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Schools;

    public interface ISchoolsService
    {
        Task<IQueryable<SchoolServiceModel>> GetAllSchoolsByDistrictId(int id);

        IQueryable<SchoolServiceModel> GetAllSchools();

        IQueryable<ClassProfileServiceModel> GetAllClassProfiles();

        IQueryable<ClassServiceModel> GetAllClasses();

        Task<SchoolServiceModel> GetSchoolDetailsById(int id);

        Task<SchoolServiceModel> GetSchoolDetailsByName(string name);

        Task<bool> CreateClassProfile(string name);

        Task<bool> EditSchool(ClassServiceModel classModel, SchoolServiceModel model);
    }
}
