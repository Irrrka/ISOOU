namespace ISOOU.Web.ViewModels.Schools
{
    using System.Collections.Generic;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class SearchFreeSpotsResultViewModel : IMapFrom<School>
    {
        public SearchFreeSpotsResultViewModel()
        {
            this.Result = new Dictionary<BaseSchoolModel, Dictionary<string, int>>();
        }

        public int Id { get; set; }

        public string DistrictName { get; set; }

        public int YearOfBirth { get; set; }

        public Dictionary<BaseSchoolModel, Dictionary<string, int>> Result { get; set; }
    }
}
