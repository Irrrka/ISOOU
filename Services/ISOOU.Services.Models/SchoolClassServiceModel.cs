using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;

namespace ISOOU.Services.Models
{
    public class SchoolClassServiceModel : IMapFrom<SchoolClass>, IMapTo<SchoolClass>
    {

        public int Id { get; set; }

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }

        public int ClassId { get; set; }

        public ClassServiceModel Class { get; set; }

        public virtual ICollection<CandidateSchoolClassServiceModel> CandidateClasses { get; set; }
    }
}