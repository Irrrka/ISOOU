namespace ISOOU.Services.Data.Contracts
{ 
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;

    public interface ISchoolsService
    {
        Task<IEnumerable<SchoolViewModel>> GetAllSchoolsByDistrictId(int id);

        //Task<IEnumerable<SchoolClassViewModel>> GetAllSchoolsByDistrictName(string name);

        Task<IEnumerable<AllSchoolsViewModel>> GetAllSchoolsAsync();

        Task<IEnumerable<AdmissionProcedureSchoolViewModel>> GetSchoolsForAdmissionProcedureAsync();

        //Task<BaseSchoolModel> GetSchoolByName(string name);

        //Task<BaseSchoolModel> GetSchoolById(int id);

        Task<IEnumerable<SchoolClassViewModel>> GetAllSchoolsByDistrictNameWithClassesAndFreeSpots(string name);

        Task<SchoolDetails> GetSchoolDetailsById(int id);

        //Task<List<SystemUser>> GetAllAdmittedCandidates();

        //Task<List<SystemUser>> GetAllNotAdmittedCandidates();
    }
}
