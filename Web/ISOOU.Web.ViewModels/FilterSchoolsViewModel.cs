using System.Collections.Generic;

namespace ISOOU.Web.ViewModels
{
    public class FilterSchoolsViewModel
    {
        public FilterSchoolsViewModel()
        {
            this.Schools = new List<FilterSchoolViewModel>();
        }

        public List<FilterSchoolViewModel> Schools { get; set; }
    }
}
