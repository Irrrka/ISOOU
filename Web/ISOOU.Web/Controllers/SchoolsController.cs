namespace ISOOU.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using ISOOU.Services.Data;
    using ISOOU.Web.ViewModels;
    using ISOOU.Data.Models;

    public class SchoolsController : Controller
    {
        private readonly ISchoolsService schoolsService;

        public SchoolsController(ISchoolsService schoolsService)
        {
            this.schoolsService = schoolsService;
        }

        [HttpGet("/Schools/AllSchoolsByDistrict/{value}")]
        public IActionResult AllSchoolsByDistrict(int value)
        {
            var schools = this.schoolsService.GetAllSchoolsByDistrict<SchoolViewModel>(value);
            return this.View(schools);
        }

        public IActionResult AllAdmittedCandidates()
        {
            //List<SystemUser> candidatesFromDb = this.schoolsService.GetAllAdmittedCandidates();
            //List<StatusCandidateViewModel> candidates = candidatesFromDb
            //    .Select(c => new StatusCandidateViewModel
            //    {
            //        Name = (c.FirstName.ToCharArray()[0] + c.MiddleName.ToCharArray()[0] + c.LastName.ToCharArray()[0]).ToString(),
            //        UniqueNumber = c.UniqueNumber,
            //    }).ToList();

            StatusCandidatesViewModel model = new StatusCandidatesViewModel();
            //foreach (var candidate in candidates)
            //{
            //    model.StatusCandidates.Add(candidate);
            //}

            return this.View(model);
        }

        public IActionResult Filter()
        {
            return this.View();
        }

        //TODO InputModel?
        [HttpPost]
        public ActionResult Filter(FilterCandidateInputModel model)
        {
            this.schoolsService.CreateFilter(model.YearOfBirth, model.District);

            var schoolsFromDb = this.schoolsService.GetFreePlacesByYearAndByDistrict(model.YearOfBirth, model.District)
                .Select(sc => new FilterSchoolViewModel
                 {
                     District = sc.District.Name,
                     Address = sc.Address.Permanent,
                     Name = sc.Name,
                     UrlOfSchool = sc.URLOfSchool,
                     UrlOfMap = sc.URLOfMap,
                     FreePlaces = sc.FreePlaces,
                 }).ToList();

            FilterSchoolsViewModel models = new FilterSchoolsViewModel();
            foreach (var school in schoolsFromDb)
            {
                models.Schools.Add(school);
            }

            return this.View(models);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}