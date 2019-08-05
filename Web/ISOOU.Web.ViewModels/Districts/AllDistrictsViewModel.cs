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

            return dict;
        }
    }
}
