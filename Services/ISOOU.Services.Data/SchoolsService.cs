namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    public class SchoolsService : ISchoolsService
    {
        private readonly ISOOUContext context;

        public SchoolsService(ISOOUContext context)
        {
            this.context = context;
        }

        public List<School> GetSchoolsByDistrict(string district)
        {
            List<School> schools = this.context.Schools
                .Where(sc => sc.Address.District == district).ToList();
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
            List<School> schoolsFromDb = this.context.Schools
                .Where(y => y.Candidates.FirstOrDefault().YearOfBirth == year)
                .Where(d => d.Address.District == district)
                .ToList();

            return schoolsFromDb;
        }

        public bool CreateFilter(int year, string district)
        { 
            //TODO or delete
            return true;
        }

        public List<SystemUser> GetAllAdmittedCandidates()
        {
            List<SystemUser> admittedCandidatesFromDb = this.context
                .Candidates
                .Where(c => c.Status.ToString() == nameof(CandidateStatus.Admitted))
                .OrderBy(s => s.Schools
                .OrderBy(a => a.Address.District)
                .ThenBy(n => n.Ref)).ToList();

            return admittedCandidatesFromDb;
        }

        public List<SystemUser> GetAllNotAdmittedCandidates()
        {
            List<SystemUser> notAdmittedCandidatesFromDb = this.context
                 .Candidates
                .Where(c => c.Status.ToString() == nameof(CandidateStatus.NotAdmitted))
                .OrderBy(s => s.Schools
                .OrderBy(a => a.Address.District)
                .ThenBy(n => n.Ref)).ToList();

            return notAdmittedCandidatesFromDb;
        }
    }
}
