namespace ISOOU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchFreeSpotsResultViewModel
    { 

        public SearchFreeSpotsResultViewModel()
        {
            this.Result = new Dictionary<SchoolForSearchResultViewModel, int>();
        }

        public string DistrictName { get; set; }

        public int YearOfBirth { get; set; }

        public List<SchoolForSearchResultViewModel> Schools { get; set; }

        public Dictionary<SchoolForSearchResultViewModel, int> Result { get; set; }
    }
}
