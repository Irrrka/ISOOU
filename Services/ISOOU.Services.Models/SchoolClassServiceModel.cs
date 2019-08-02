using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class SchoolClassServiceModel : IMapFrom<SchoolClass>, IMapTo<SchoolClass>
    {

        public int Id { get; set; }

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }

        public int ClassId { get; set; }

        public ClassServiceModel Class { get; set; }
    }
}