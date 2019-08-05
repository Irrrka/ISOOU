namespace ISOOU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Schools;

    public class SearchFreeSpotsResultViewModel
    { 
        public SearchFreeSpotsResultViewModel()
        {
            this.Result = new Dictionary<SchoolForSearchResultViewModel, List<int>>();
        }

        public string DistrictName { get; set; }

        public int YearOfBirth { get; set; }

        public List<SchoolForSearchResultViewModel> Schools { get; set; }

        public Dictionary<SchoolForSearchResultViewModel, List<int>> Result { get; set; }
    }
}
