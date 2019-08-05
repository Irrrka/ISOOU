using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Services.Models
{
    public class SchoolServiceModel : IMapFrom<School>, IMapTo<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirectorName { get; set; }

        public string Address { get; set; }

        public virtual DistrictServiceModel District { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public virtual ICollection<SchoolClassServiceModel> SchoolClasses { get; set; }
    }
}
