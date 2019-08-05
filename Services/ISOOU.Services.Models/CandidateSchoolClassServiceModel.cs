using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class CandidateSchoolClassServiceModel : IMapFrom<CandidateSchoolClass>, IMapTo<CandidateSchoolClass>
    {
        public int CandidateId { get; set; }

        public virtual CandidateServiceModel Candidate { get; set; }

        public int SchoolClassId { get; set; }

        public virtual SchoolClassServiceModel SchoolClass { get; set; }
    }
}