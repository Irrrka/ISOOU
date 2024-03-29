﻿namespace ISOOU.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Districts;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : DirectorsController
    {
        private readonly ISchoolsService schoolsService;
        private readonly ICandidatesService candidatesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDistrictsService districtsService;
        private readonly UserManager<SystemUser> userManager;

        public HomeController(
            ISchoolsService schoolsService,
            IDistrictsService districtsService,
            ICloudinaryService cloudinaryService,
            ICandidatesService candidatesService,
            UserManager<SystemUser> userManager)
        {
            this.schoolsService = schoolsService;
            this.districtsService = districtsService;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.candidatesService = candidatesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        //[HttpGet]
        //public ActionResult CreateClassProfile()
        //{
        //    return this.View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateClassProfile(string name)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View();
        //    }

        //    await this.schoolsService.CreateClassProfile(name);

        //    return this.Redirect("/");
        //}

        //[HttpGet]
        //public ActionResult EditSchool([FromQuery(Name = "name")] string name)
        //{
        //    IQueryable<SchoolServiceModel> allSchools = this.schoolsService.GetAllSchools();
        //    this.ViewData["Schools"] = allSchools.To<BaseSchoolModel>().ToList();

        //    if (name == null)
        //    {
        //        return this.View();
        //    }
        //    else
        //    {
        //        var schoolToEdit = allSchools
        //                            .FirstOrDefault(x => x.Name == name)
        //                            .To<EditSchoolViewModel>();
        //        return this.View(schoolToEdit);
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> EditSchool()
        {
            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts.Select(d => new EditSchoolDistrictModel { Name = d.Name }).ToList();

            var userIdentity = this.User.Identity.Name;

            var model = (await this.schoolsService.GetSchoolForEdit(userIdentity)).To<EditSchoolInputModel>();

            if (model == null)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult EditSchool(int id, EditSchoolInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var allDistricts = this.districtsService.GetAllDistricts();
                this.ViewData["Districts"] = allDistricts.Select(d => new EditSchoolDistrictModel { Name = d.Name }).ToList();

                return this.View(input);
            }

            SchoolServiceModel schoolToEdit = input.To<SchoolServiceModel>();

            this.schoolsService.EditSchool(id, schoolToEdit);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> ViewDocuments()
        {
            var userIdentity = this.User.Identity.Name;

            SchoolServiceModel directorsSchool = await this.schoolsService.GetSchoolForEdit(userIdentity);

            if (directorsSchool == null)
            {
                return this.Redirect("/");
            }

            var documents = this.cloudinaryService
                .ViewDocuments(directorsSchool.Id)
                .To<ViewDocumentViewModel>()
                .ToList();

            return this.View(documents);
        }

    }
}
