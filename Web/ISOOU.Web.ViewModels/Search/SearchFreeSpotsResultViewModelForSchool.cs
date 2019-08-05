namespace ISOOU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class SearchFreeSpotsResultViewModelForSchool : IMapFrom<School>
    {
        public SearchFreeSpotsResultViewModelForSchool()
        {
            this.FreeSpotsByClassLanguageType = new Dictionary<string, int>();
        }

        public int Id { get; set; }

        public string SchoolName { get; set; }

        public string DistrictName { get; set; }

        public int YearOfBirth { get; set; }

        public Dictionary<string, int> FreeSpotsByClassLanguageType { get; set; }
    }
}
