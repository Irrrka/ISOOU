namespace ISOOU.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Data;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;

    public class SchoolsController : Controller
    {
        private readonly ISchoolsService schoolsService;
        private readonly IDistrictsService districtsService;
        private readonly ISearchService searchService;
        private readonly ICandidatesService candidatesService;

        public SchoolsController(ISchoolsService schoolsService, ICandidatesService candidatesService, IDistrictsService districtsService, ISearchService searchService)
        {
            this.schoolsService = schoolsService;
            this.candidatesService = candidatesService;
            this.districtsService = districtsService;
            this.searchService = searchService;
        }

        [HttpGet("/Schools/AllSchoolsByDistrict/{id}")]
        public async Task<IActionResult> AllSchoolsByDistrict(int id)
        {
            IEnumerable<SchoolViewModel> schools = await this.schoolsService.GetAllSchoolsByDistrictId(id);
            return this.View(schools);
        }

        public async Task<IActionResult> Index()
        {
            await this.All();
            return this.View("All");
        }

        public async Task<IActionResult> All()
        {
            var schools = await this.schoolsService.GetAllSchoolsAsync();

            return this.View(schools);
        }

        [HttpGet("/Schools/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            SchoolDetails model = await this.schoolsService.GetSchoolDetailsById(id);

            return this.View(model);
        }
    }
}