using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class ClassProfileServiceModel : IMapFrom<ClassProfile>, IMapTo<ClassProfile>
    {
            public int Id { get; set; }

            public string Name { get; set; }
    }
}