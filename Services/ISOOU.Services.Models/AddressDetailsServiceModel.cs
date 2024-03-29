﻿namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Mapping;

    public class AddressDetailsServiceModel : IMapFrom<AddressDetails>, IMapTo<AddressDetails>
    {
        public int Id { get; set; }

        public CityName PermanentCity { get; set; }

        public CityName CurrentCity { get; set; }

        public string Permanent { get; set; }

        public string Current { get; set; }

        public int CurrentDistrictId { get; set; }

        public DistrictServiceModel CurrentDistrict { get; set; }

        public int PermanentDistrictId { get; set; }

        public DistrictServiceModel PermanentDistrict { get; set; }
    }
}
