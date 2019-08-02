namespace ISOOU.Web.ViewModels.Schools
{
    using System.Collections.Generic;

    public class SchoolDetailsWithSpotsAndCandidatesViewModel : BaseSchoolModel
    {
        public string Address { get; set; }

        public string DistrictName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public List<string> NotAdmittedCandidatesUniqueNumber { get; set; }

        public List<string> AdmittedCandidatesUniqueNumber { get; set; }

        public List<int> PossibleYears { get; set; }

        //public Dictionary<string, int> SpotsByClasses { get; set; }

        public string AdmissionProcedureStatus { get; set; }

        public int AdmissionProcedureStatusCount { get; set; }
    }
}
