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

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IDistrictsService districtsService;
        private readonly ISchoolsService schoolsService;

        public SearchController(
            ISearchService searchService,
            IDistrictsService districtsService,
            ISchoolsService schoolsService)
        {
            this.searchService = searchService;
            this.districtsService = districtsService;
            this.schoolsService = schoolsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult FreeSpots()
        {
            var inputModel = new SearchFreeSpotsInputModel();

            var allPossibleYears = FreeSpotsCenter.GetAllPossibleYears();
            this.ViewData["Years"] = allPossibleYears;

            var allDistricts = this.districtsService.GetAllDistricts().ToList().To<SearchDistrictViewModel>();
            this.ViewData["Districts"] = allDistricts;

            return this.View();
        }

        [HttpPost]
        //[Route("/Search/FreeSpots/{selectedYearOfBirth}&{selectedDistrictId}")]
        public async Task<IActionResult> FreeSpots(SearchFreeSpotsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var allPossibleYears = FreeSpotsCenter.GetAllPossibleYears();
            this.ViewData["Years"] = allPossibleYears;

            var allDistricts = this.districtsService.GetAllDistricts().ToList().To<SearchDistrictViewModel>();
            this.ViewData["Districts"] = allDistricts;

            var allPossibleClassProfiles = this.schoolsService.GetAllClassProfiles()
                .Select(x=>x.Name)
                .OrderBy(name => name).ToList();
            this.ViewData["Classes"] = allPossibleClassProfiles;


            int selectedYearOfBirth = input.SelectedYearOfBirth;
            int selectedDistrictId = input.SelectedDistrictId;

            var result = await this.searchService.GetSearchResult(selectedDistrictId, selectedYearOfBirth);

            return this.View("FreeSpotsByYearAndByDistrict", result);
        }
    }
}