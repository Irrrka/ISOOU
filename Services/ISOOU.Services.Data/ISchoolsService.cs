namespace ISOOU.Services.Data
{
    using System.Collections.Generic;

    using ISOOU.Data.Models;

    public interface ISchoolsService
    {
        IEnumerable<TSchoolViewModel> GetAllSchoolsByDistrict<TSchoolViewModel>(int value);

        List<SystemUser> GetAllAdmittedCandidates();

        List<SystemUser> GetAllNotAdmittedCandidates();

    }
}
