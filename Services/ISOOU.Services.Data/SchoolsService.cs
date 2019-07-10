namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    public class SchoolsService : ISchoolsService
    {
        private readonly ISOOUDbContext context;

        public SchoolsService(ISOOUDbContext context)
        {
            this.context = context;
        }

        public List<School> GetSchoolsByDistrict(string district)
        {
            List<School> schools = this.context.Schools
                .Where(sc => sc.AddressDetails.District == district).ToList();
            return schools;
        }

        public List<SchoolsDistrict> GetAllDisctricts()
        {
            //TODO
            return null;
        }

        //TODO
        public List<School> GetFreePlacesByYearAndByDistrict(int year, string district)
        {
           

            return null;
        }

        public bool CreateFilter(int year, string district)
        { 
            //TODO or delete
            return true;
        }

        public List<SystemUser> GetAllAdmittedCandidates()
        {
            //List<SystemUser> admittedCandidatesFromDb = this.context
            //    .Candidates
            //    .Where(c => c.Status.ToString() == nameof(CandidateStatus.Admitted))
            //    .OrderBy(s => s.Schools
            //    .OrderBy(a => a.Address.District)
            //    .ThenBy(n => n.Ref)).ToList();

            return null;
        }

        public List<SystemUser> GetAllNotAdmittedCandidates()
        {
            //List<SystemUser> notAdmittedCandidatesFromDb = this.context
            //     .Candidates
            //    .Where(c => c.Status.ToString() == nameof(CandidateStatus.NotAdmitted))
            //    .OrderBy(s => s.Schools
            //    .OrderBy(a => a.Address.District)
            //    .ThenBy(n => n.Ref)).ToList();

            return null;
        }
    }
}
