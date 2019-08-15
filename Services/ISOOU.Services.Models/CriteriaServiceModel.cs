namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class CriteriaServiceModel : IMapFrom<Criteria>, IMapTo<Criteria>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Scores { get; set; }

        //public SystemUser User { get; set; }

        public int CandidateId { get; set; }

        public CandidateServiceModel Candidate { get; set; }
    }
}
