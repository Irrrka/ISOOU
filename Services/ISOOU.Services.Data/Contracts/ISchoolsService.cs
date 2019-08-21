namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface ISchoolsService
    {
        Task<IQueryable<SchoolServiceModel>> GetAllSchoolsByDistrictId(int id);

        IQueryable<SchoolServiceModel> GetAllSchools();

        Task<SchoolServiceModel> GetSchoolDetailsById(int id);

        Task<int> GetSchoolIdByName(string name);

        Task<bool> EditSchool(int id, SchoolServiceModel model);

        Task<SchoolServiceModel> GetSchoolForEdit(string userIdentity);

        IQueryable<CandidateApplicationServiceModel> GetSchoolsAndCandidates();
    }
}
