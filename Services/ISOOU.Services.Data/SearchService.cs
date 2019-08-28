namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Search;

    //TODO Refactor ServiceModel
    public class SearchService : ISearchService
    {
        private readonly ISchoolsService schoolsService;

        public SearchService(
            ISchoolsService schoolsService)
        {
            this.schoolsService = schoolsService;
        }

        public async Task<SearchFreeSpotsResultViewModel> GetSearchResult(List<int> districtIds)
        {
            var result = new SearchFreeSpotsResultViewModel();

            for (int i = 0; i < districtIds.Count; i++)
            {
                var schoolsVM = (await this.schoolsService
                                           .GetAllSchoolsByDistrictId(districtIds[i]))
                                           .To<SchoolForSearchResultViewModel>();
                result.SchoolsVM.AddRange(schoolsVM);
                var districtName = schoolsVM.Select(d => d.DistrictName).FirstOrDefault();
                result.Districts.Add(districtName);
            }

            result.Districts.Distinct();
            result.SchoolsVM.Distinct();

            return result;
        }
    }
}
