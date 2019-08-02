using AutoMapper;
using ISOOU.Data.Models;
using ISOOU.Data.Models.Enums;
using ISOOU.Services.Mapping;
using System.Collections.Generic;

namespace ISOOU.Services.Models
{
    public class ParentServiceModel : IMapFrom<Parent>, IMapTo<Parent>, IHaveCustomMappings
    {
        public ParentServiceModel()
           : base()
        {
            this.Candidates = new HashSet<CandidateParentsServiceModel>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string UCN { get; set; }

        public string PhoneNumber { get; set; }

        public virtual AddressDetailsServiceModel Address { get; set; }

        public string UserId { get; set; }

        public string Role { get; set; }

        public string WorkName { get; set; }

        public virtual DistrictServiceModel WorkDistrict { get; set; }

        public virtual ICollection<CandidateParentsServiceModel> Candidates { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //role, user
        }
    }
}
