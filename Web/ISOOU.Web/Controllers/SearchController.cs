namespace ISOOU.Web.Controllers
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Data;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //TODO Refactor SearchFunctionality
    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;
        private readonly IDistrictsService districtsService;
        private readonly ISchoolsService schoolsService;
        private readonly ICalculatorService calculatorService;

        public SearchController(
            ISearchService searchService,
            ICalculatorService calculatorService,
            IDistrictsService districtsService,
            ISchoolsService schoolsService)
        {
            this.searchService = searchService;
            this.districtsService = districtsService;
            this.schoolsService = schoolsService;
            this.calculatorService = calculatorService;
        }

        [HttpGet]
        public IActionResult FreeSpots()
        {
            var allDistricts = this.districtsService.GetAllDistricts().To<SearchDistrictViewModel>();
            this.ViewData["Districts"] = allDistricts.ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> FreeSpots(SearchFreeSpotsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var allDistricts = this.districtsService.GetAllDistricts().To<SearchDistrictViewModel>();
                this.ViewData["Districts"] = allDistricts.ToList();

                return this.View();
            }

            List<int> districtIds = new List<int>();

            //Permanent
            if (!districtIds.Contains(input.SelectedPermanentDistrictId))
            {
                districtIds.Add(input.SelectedPermanentDistrictId);
            }

            //Current
            if (!districtIds.Contains(input.SelectedCurrentDistrictId))
            {
                districtIds.Add(input.SelectedCurrentDistrictId);
            }

            //Work
            if (!districtIds.Contains(input.SelectedWorkDistrictId))
            {
                districtIds.Add(input.SelectedWorkDistrictId);
            }

            var result = await this.searchService
                                    .GetSearchResult(districtIds);

            return this.View("FreeSpotsByDistrict", result);
        }
    }
}