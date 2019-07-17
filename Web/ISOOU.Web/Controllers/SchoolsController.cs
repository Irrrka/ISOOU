﻿namespace ISOOU.Web.Controllers
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Data;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SchoolsController : Controller
    {
        private readonly ISchoolsService schoolsService;
        private readonly IDistrictsService districtsService;
        private readonly ISearchSpotsService searchSpotsService;
        private readonly ICandidatesService candidatesService;

        public SchoolsController(ISchoolsService schoolsService, ICandidatesService candidatesService, IDistrictsService districtsService, ISearchSpotsService searchSpotsService)
        {
            this.schoolsService = schoolsService;
            this.candidatesService = candidatesService;
            this.districtsService = districtsService;
            this.searchSpotsService = searchSpotsService;
        }

        [HttpGet("/Schools/AllSchoolsByDistrict/{value}")]
        public async Task<IActionResult> AllSchoolsByDistrict(int value)
        {
            var schools = await this.schoolsService.GetAllSchoolsByDistrictValue(value);
            return this.View(schools);
        }

        public IActionResult AllAdmittedCandidates()
        {
            return this.View();
        }

        public async Task<IActionResult> SearchSpots()
        {
            var districtViewModels = await this.districtsService.GetAllDistrictsAsync();
            var searchViewModels = districtViewModels.Select(m => new SearchSpotsViewModel
            {
                District = m.Name,
            }).ToList();
            this.ViewData["Districts"] = searchViewModels;
            var yearsViewModels = this.searchSpotsService.GetAllPossibleYears();
            this.ViewData["Years"] = yearsViewModels;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchSpots(SearchSpotsViewModel model)
        {
            var models = await this.searchSpotsService.GetSearchResultAsync(model.District, model.YearOfBirth);
            return this.View("SpotsByYearAndByDistrict", models);
        }

        public async Task<IActionResult> All()
        {
            var schools = await this.schoolsService.GetAllSchoolsAsync();

            return this.View(schools);
        }

        public async Task<IActionResult> Index()
        {
            await this.All();
            return this.View("All");
        }


        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

    }
}