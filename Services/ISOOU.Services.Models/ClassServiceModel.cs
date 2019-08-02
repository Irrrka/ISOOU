using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;

namespace ISOOU.Services.Models
{
    public class ClassServiceModel : IMapFrom<Class>, IMapTo<Class>
    {
        public ClassProfileServiceModel Profile { get; set; }

        public int FreeSpots { get; set; }

        public virtual ICollection<SchoolClassServiceModel> SchoolClasses { get; set; }
    }
}