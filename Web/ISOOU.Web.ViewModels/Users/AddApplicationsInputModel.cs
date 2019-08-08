using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Users
{
    public class AddApplicationsInputModel
    {
        public int CandidateId { get; set; }

        public string UserName { get; set; }

        public string FirstWishSchool { get; set; }

        public string FirstWishClassProfile { get; set; }

        public string SecondWishSchool { get; set; }

        public string SecondWishClassProfile { get; set; }

        public string ThirdWishSchool { get; set; }

        public string ThirdWishClassProfile { get; set; }
    }
}
