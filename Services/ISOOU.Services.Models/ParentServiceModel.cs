namespace ISOOU.Services.Models
{
    using AutoMapper;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Mapping;
    using System.Collections.Generic;

    public class ParentServiceModel : IMapFrom<Parent>, IMapTo<Parent>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UCN { get; set; }

        public string UserId { get; set; }

        public SystemUserServiceModel User { get; set; }

        public ParentRole Role { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public AddressDetailsServiceModel Address { get; set; }

        public string WorkName { get; set; }

        public int WorkDistrictId { get; set; }

        public DistrictServiceModel WorkDistrict { get; set; }

        public ICollection<CandidateServiceModel> Candidates { get; set; }

    }
}
