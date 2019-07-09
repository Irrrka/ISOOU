namespace ISOOU.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ISOOU.Data.Models.Enums;

    public class DistrictViewModel
    {
        public DistrictViewModel()
        {
            this.Districts = Enum.GetValues(typeof(SchoolsDistrict)).Cast<SchoolsDistrict>().ToList();
        }

        public List<SchoolsDistrict> Districts { get; set; }
    }
}
