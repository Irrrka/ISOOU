using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class CriteriaServiceModel : IMapFrom<Criteria>, IMapTo<Criteria>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Scores { get; set; }

        public SystemUserServiceModel User { get; set; }

        public int CandidateId { get; set; }

        public CandidateServiceModel Candidate { get; set; }
    }
}