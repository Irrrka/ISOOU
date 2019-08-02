namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models.Enums;

    public class AddressDetails : BaseModel<int>
    {
        public CityName PermanentCity { get; set; }

        public CityName CurrentCity { get; set; }

        public string Permanent { get; set; }

        public string Current { get; set; }

        public int CurrentDistrictId { get; set; }

        public virtual District CurrentDistrict { get; set; }

        public int PermanentDistrictId { get; set; }

        public virtual District PermanentDistrict { get; set; }
    }
}
