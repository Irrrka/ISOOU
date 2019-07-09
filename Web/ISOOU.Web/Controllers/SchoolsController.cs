namespace ISOOU.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using ISOOU.Data.Models;
    using ISOOU.Services.Data;
    using ISOOU.Web.ViewModels;

    public class SchoolsController : Controller
    {
        private readonly ISchoolsService schoolsService;

        public SchoolsController(ISchoolsService schoolsService)
        {
            this.schoolsService = schoolsService;
        }

        // GET: SchoolsByDistrict
        public ActionResult AllSchoolsByDistrict(string district)
        {
            var schoolsFromDb = this.schoolsService.GetSchoolsByDistrict(district);
            List<SchoolViewModel> schools = schoolsFromDb
                .Select(sc => new SchoolViewModel
                {
                    District = district,
                    Name = sc.Ref + " ОУ " + sc.Name,
                    Director = sc.Director.FirstName + " " + sc.Director.LastName,
                    Address = sc.Address.Permanent,
                    Email = sc.Director.Email,
                    PhoneNumber = sc.Director.PhoneNumber.ToString(),
                    UrlOfSchool = sc.URLOfSchool,
                }).ToList();

            SchoolsViewModel model = new SchoolsViewModel();
            foreach (var school in schools)
            {
                model.Schools.Add(school);
            }

            return this.View(model);
        }

        // GET: Schools/Filter
        public ActionResult Filter()
        {
            return this.View();
        }

        // GET: AllAdmittedCandidates
        public ActionResult AllAdmittedCandidates()
        {
            List<SystemUser> candidatesFromDb = this.schoolsService.GetAllAdmittedCandidates();
            List<StatusCandidateViewModel> candidates = candidatesFromDb
                .Select(c => new StatusCandidateViewModel
                {
                    Name = (c.FirstName.ToCharArray()[0] + c.MiddleName.ToCharArray()[0] + c.LastName.ToCharArray()[0]).ToString(),
                    UniqueNumber = c.UniqueNumber,
                }).ToList();

            StatusCandidatesViewModel model = new StatusCandidatesViewModel();
            foreach (var candidate in candidates)
            {
                model.StatusCandidates.Add(candidate);
            }

            return this.View(model);
        }

        // GET: AllNotAdmittedCandidates
        public ActionResult AllNotAdmittedCandidates()
        {
            List<SystemUser> candidatesFromDb = this.schoolsService.GetAllNotAdmittedCandidates();
            List<StatusCandidateViewModel> candidates = candidatesFromDb
                .Select(c => new StatusCandidateViewModel
                {
                    Name = (c.FirstName.ToCharArray()[0] + c.MiddleName.ToCharArray()[0] + c.LastName.ToCharArray()[0]).ToString(),
                    UniqueNumber = c.UniqueNumber,
                }).ToList();

            StatusCandidatesViewModel model = new StatusCandidatesViewModel();
            foreach (var candidate in candidates)
            {
                model.StatusCandidates.Add(candidate);
            }

            return this.View(model);
        }


        //TODO InputModel?
        // Post: Schools/Filter
        [HttpPost]
        public ActionResult Filter(FilterCandidateInputModel model)
        {
            this.schoolsService.CreateFilter(model.YearOfBirth, model.District);

            var schoolsFromDb = this.schoolsService.GetFreePlacesByYearAndByDistrict(model.YearOfBirth, model.District)
                .Select(sc => new FilterSchoolViewModel
                 {
                     District = sc.Address.District,
                     Address = sc.Address.Permanent,
                     Name = sc.Ref + " ОУ " + sc.Name,
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


        // GET: Schools/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        // POST: Schools/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Schools/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schools/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Schools/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schools/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}