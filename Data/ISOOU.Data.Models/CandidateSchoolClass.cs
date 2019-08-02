namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;

    public class CandidateSchoolClass : BaseModel<int>
    {
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public int SchoolClassId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
    }
}
