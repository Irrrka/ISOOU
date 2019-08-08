namespace ISOOU.Web.Areas.Administration.Controllers
{
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Data;
    using ISOOU.Web.Areas.Administration.ViewModels.Dashboard;
    using ISOOU.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ISOOU.Data.Models;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Users;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using ISOOU.Web.Areas.Users.Models;
    using ISOOU.Common;
    using ISOOU.Web.ViewModels.Home;
    using ISOOU.Services.Models;

    public class DashboardController : AdministrationController
    {
        private readonly IAdminService adminService;
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<SystemUser> userManager;

        public DashboardController(
            IAdminService adminService,
            ISchoolsService schoolsService,
            UserManager<SystemUser> userManager)
        {
            this.adminService = adminService;
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            QuestionServiceModel message = await this.adminService.ReadLastMessage();
            
            return this.View(message);
        }

        [HttpGet]
        public ActionResult CreateDirector()
        {
            IQueryable<SchoolServiceModel> allSchools = this.schoolsService.GetAllSchools();
            this.ViewData["Schools"] = allSchools.To<CreateDirectorSchoolModel>().ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDirector(CreateDirectorInputModel createDirectorInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createDirectorInputModel);
            }

            var user = new SystemUser
            {
                UserName = createDirectorInputModel.Email,
                Email = createDirectorInputModel.Email,
                FullName = createDirectorInputModel.FirstName + " " + createDirectorInputModel.LastName,
                UCN = createDirectorInputModel.UCN,
                AdmissionSchoolId = createDirectorInputModel.School,
            };

            var result = await this.userManager.CreateAsync(user, createDirectorInputModel.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.DirectorRoleName);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<ActionResult> StartProcedure()
        {
            //TODO ServiceModel
            var dataFromDbForProcedure = await this.adminService.StartAdmissionProcedure();
            //var possibleYears = FreeSpotsCenter.GetAllPossibleYears();
            
            //status message KLASIRANETO E IZVYRSHENO???
            return this.View(dataFromDbForProcedure);
        }


    }
}
