using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Schools
{
    public class AddApplicationsAllSchoolsByDistrictViewModel
    {
        public AddApplicationsAllSchoolsByDistrictViewModel()
        {
            this.ScoresBySchool = new Dictionary<string, int>();
        }

        public int Id { get; set; }

        public Dictionary<string, int> ScoresBySchool { get; set; }

    }
}
