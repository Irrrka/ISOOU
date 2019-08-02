namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;

    public class Criteria : BaseModel<int>
    {
        public string Name { get; set; }

        public int Scores { get; set; }

        public SystemUser User { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}
