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

        public int Id { get; set; }

        public string SchoolName { get; set; }

        public int SchoolId { get; set; }

        public int NumberOfApplication { get; set; }

        //public int SchoolDistrictId { get; set; }

        //public string SchoolDistrictName { get; set; }

        public SortedDictionary<string, int> ScoresBySchoolNames { get; set; }

        //public string IsAdmittedStatusMessage { get; set; }
    }
}
