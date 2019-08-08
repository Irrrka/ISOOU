namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;

    public class SchoolCandidate : BaseModel<int>
    {
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }
    }
}
