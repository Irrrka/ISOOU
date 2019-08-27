
namespace ISOOU.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Mvc;

    public class SchoolsController : BaseController
    {
        private readonly ISchoolsService schoolsService;

        public SchoolsController(ISchoolsService schoolsService)
        {
            this.schoolsService = schoolsService;
        }

        [HttpGet("/Schools/AllSchoolsByDistrict/{id}")]
        public async Task<IActionResult> AllSchoolsByDistrict(int id)
        {
            var schools = (await this.schoolsService.GetAllSchoolsByDistrictId(id))
                        .To<SchoolByDistrictViewModel>()
                        .ToList();

            return this.View(schools);
        }

        public IActionResult Index()
        {
            this.All();
            return this.View("All");
        }

        public IActionResult All()
        {
            var schools = this.schoolsService
                .GetAllSchools()
                .To<AllSchoolsViewModel>()
                .ToList();

            return this.View(schools);
        }

        [HttpGet("/Schools/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var sm = await this.schoolsService.GetSchoolDetailsById(id);

            //TODO CoefOfYear?
            var model = sm.To<SchoolDetailsViewModel>();
            //model.FreeSpots = model.FreeSpots * (FreeSpotsCenter.CalculateCoeficient());

            return this.View(model);
        }
    }
}