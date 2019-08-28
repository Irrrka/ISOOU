namespace ISOOU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchFreeSpotsResultViewModel
    {
        public SearchFreeSpotsResultViewModel()
        {
            this.SchoolsVM = new List<SchoolForSearchResultViewModel>();
            this.Districts = new List<string>();
        }

        public List<string> Districts { get; set; }

        public List<SchoolForSearchResultViewModel> SchoolsVM { get; set; }

    }
}
