namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;

    public interface ISchoolsService
    {
        Task<IEnumerable<SchoolViewModel>> GetAllSchoolsByDistrictValue(int value);

        Task<IEnumerable<SchoolClassesViewModel>> GetAllSchoolsByDistrictName(string districtName);

        Task<IEnumerable<AllSchoolsViewModel>> GetAllSchoolsAsync();

        Task<BaseSchoolModel> GetSchoolByName(string name);

        IEnumerable<ClassViewModel> GetAllClasses();

        //Task<List<SystemUser>> GetAllAdmittedCandidates();

        //Task<List<SystemUser>> GetAllNotAdmittedCandidates();
    }
}
