namespace ISOOU.Web.ViewModels
{
    using ISOOU.Web.ViewModels.Schools;

    using System.Collections.Generic;

    public class SchoolViewModel : BaseSchoolModel
    {
        public SchoolViewModel()
        {
            this.FreeSpotsByLanguageTypeOfSchoolClasses = new Dictionary<string, int>();
        }

        public string DirectorName { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string UrlOfSchool { get; set; }

        public string UrlOfMap { get; set; }

        //TODO use?
        public IDictionary<string, int> FreeSpotsByLanguageTypeOfSchoolClasses { get; set; }

    }
}
