namespace ISOOU.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models.Enums;

    public class AddressDetails : BaseModel<int>
    {
        public CityName PermanentCity { get; set; }

        public CityName CurrentCity { get; set; }

        public string PermanentAddress { get; set; }

        public string CurrentAddress { get; set; }

        public int CurrentDistrictId { get; set; }

        public virtual District CurrentDistrict { get; set; }

        public int PermanentDistrictId { get; set; }

        public virtual District PermanentDistrict { get; set; }
    }
}