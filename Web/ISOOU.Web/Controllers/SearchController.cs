namespace ISOOU.Web.Controllers
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IDistrictsService districtsService;

        public SearchController(ISearchService searchService, IDistrictsService districtsService)
        {
            this.searchService = searchService;
            this.districtsService = districtsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult FreeSpots()
        {
            var inputModel = new SearchFreeSpotsInputModel();
            var districtViewModels = this.districtsService.GetAllDistrictsAsync();

            List<int> years = FreeSpotsCenter.GetAllPossibleYears().ToList();
            List<string> districtNames = districtViewModels.Select(m => m.Name).ToList();

            foreach (var dn in districtNames)
            {
                inputModel.Districts.Add(dn);
            }

            foreach (var y in years)
            {
                inputModel.YearOfBirths.Add(y);
            }

            return this.View(inputModel);
        }

        [HttpPost]
        //[Route("/FreeSpots/year={result.DistrictName}&district={result.YearOfBirth}")]
        public async Task<IActionResult> FreeSpots(SearchFreeSpotsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var result = await this.searchService.GetSearchResultAsync(input.SelectedDistrict, input.SelectedYearOfBirth);

            return this.View($"FreeSpotsByYearAndByDistrict", result);
        }
    }
}