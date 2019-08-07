using ISOOU.Data.Models;
using ISOOU.Data.Models.Enums;
using ISOOU.Services.Mapping;
using System;

namespace ISOOU.Services.Models
{
    public class AddressDetailsServiceModel : IMapFrom<AddressDetails>, IMapTo<AddressDetails>
    {
        public int Id { get; set; }

        public CityName PermanentCity { get; set; }

        public CityName CurrentCity { get; set; }

        public string Permanent { get; set; }

        public string Current { get; set; }

        public virtual DistrictServiceModel CurrentDistrict { get; set; }

        public virtual DistrictServiceModel PermanentDistrict { get; set; }
    }
}
