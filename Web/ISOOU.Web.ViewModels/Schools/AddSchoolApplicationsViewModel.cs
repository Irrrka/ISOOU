using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System.Collections.Generic;

namespace ISOOU.Web.ViewModels.Schools
{
    public class AddSchoolApplicationsViewModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>
    {
        public AddSchoolApplicationsViewModel()
        {
            this.ScoresByNumberOfApplication = new Dictionary<int, int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string DistrictName { get; set; }

        public Dictionary<int, int> ScoresByNumberOfApplication { get; set; }

    }
}
