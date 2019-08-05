using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Users
{
    public class ScoresByApplicationsViewModel
    {
        public ScoresByApplicationsViewModel()
        {
            this.ScoresBySchoolNames = new SortedDictionary<string, int>();
        }

        public SortedDictionary<string, int> ScoresBySchoolNames { get; set; }

        public string IsAdmittedStatusMessage { get; set; }
    }
}
