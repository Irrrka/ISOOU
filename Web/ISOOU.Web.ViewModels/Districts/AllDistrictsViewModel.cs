namespace ISOOU.Web.ViewModels.Districts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ISOOU.Data.Models.Enums;

    public class AllDistrictsViewModel
    {
        public AllDistrictsViewModel()
        {
            this.Districts = this.FillData();
        }

        public Dictionary<int, string> Districts { get; set; }

        private Dictionary<int, string> FillData()
        {
            Dictionary<int, string> dict = null;

            foreach (DistrictName district in Enum.GetValues(typeof(DistrictName)))
            {
                dict = Enum.GetValues(typeof(DistrictName))
                               .Cast<DistrictName>()
                               .ToDictionary(t => (int)t, t => t.ToString());
            }

            return dict;
        }
    }
}
