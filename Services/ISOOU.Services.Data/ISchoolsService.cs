using ISOOU.Data.Models;
using System.Collections.Generic;

namespace ISOOU.Services.Data
{
    public interface ISchoolsService
    {
        List<School> GetSchoolsByDistrict(string district);

        List<School> GetFreePlacesByYearAndByDistrict(int year, string district);

        //TODO or delete
        bool CreateFilter(int year, string district);

        List<SystemUser> GetAllAdmittedCandidates();

        List<SystemUser> GetAllNotAdmittedCandidates();
    }
}
