namespace ISOOU.Data.Models
{
    public class CandidateApplication
    {
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public int AdditionalScoresForSchool { get; set; }

        public int TotalScoresForSchool { get; set; }

        public int? AdmissionProcedureId { get; set; }

        public virtual AdmissionProcedure AdmissionProcedure { get; set; }
    }
}
