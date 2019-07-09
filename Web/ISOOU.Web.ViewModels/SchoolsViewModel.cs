
namespace ISOOU.Web.ViewModels
{
    using System.Collections.Generic;

    public class SchoolsViewModel
    {
        public SchoolsViewModel()
        {
            this.Schools = new List<SchoolViewModel>();
        }

        public List<SchoolViewModel> Schools { get; set; }
    }
}
