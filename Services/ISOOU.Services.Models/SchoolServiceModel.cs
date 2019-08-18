using System.Collections.Generic;

using ISOOU.Data.Common.Models;
using ISOOU.Data.Models;
using ISOOU.Services.Mapping;

namespace ISOOU.Services.Models
{
    public class SchoolServiceModel : IMapFrom<School>, IMapTo<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DistrictServiceModel District { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public int FreeSpots { get; set; }

        public ICollection<CandidateApplicationServiceModel> SchoolCandidates { get; set; }
    }
}