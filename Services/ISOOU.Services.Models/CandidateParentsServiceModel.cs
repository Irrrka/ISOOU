using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class CandidateParentsServiceModel : IMapFrom<CandidateParents>, IMapTo<CandidateParents>
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public CandidateServiceModel Candidate { get; set; }

        public int ParentId { get; set; }

        public ParentServiceModel Parent { get; set; }
    }
}