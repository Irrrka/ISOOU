namespace ISOOU.Web.ViewModels
{
    using System.Collections.Generic;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class SearchFreeSpotsInputModel : IMapFrom<Candidate>, IMapFrom<District>
    {
        public SearchFreeSpotsInputModel()
        {
            this.YearOfBirths = new List<int>();
            this.Districts = new List<string>();
        }

        public int Id { get; set; }

        public List<int> YearOfBirths { get; set; }

        public int SelectedYearOfBirth { get; set; }

        public List<string> Districts { get; set; }

        public string SelectedDistrict { get; set; }
    }
}
