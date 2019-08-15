using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{ 
    public class DistrictServiceModel : IMapFrom<District>, IMapTo<District>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
