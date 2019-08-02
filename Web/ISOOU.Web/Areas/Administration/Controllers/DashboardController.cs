namespace ISOOU.Web.Areas.Administration.Controllers
{
    using ISOOU.Services.Data.Contracts;
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

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IAdminService adminService;
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<SystemUser> userManager;

        public DashboardController(
            ISettingsService settingsService,
            IAdminService adminService,
            ISchoolsService schoolsService,
            UserManager<SystemUser> userManager)
        {
            this.settingsService = settingsService;
            this.adminService = adminService;
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDirector(CreateDirectorInputModel createDirectorInputModel)
        {
            var user = new SystemUser
            {
                UserName = createDirectorInputModel.Email,
                Email = createDirectorInputModel.Email,
                FullName = createDirectorInputModel.FirstName + " " + createDirectorInputModel.LastName,
                UCN = createDirectorInputModel.UCN,
            };

            var result = await this.userManager.CreateAsync(user, createDirectorInputModel.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.DirectorRoleName);
            }

            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> StartProcedure()
        {
            Dictionary<School, Dictionary<ClassLanguageType, List<Candidate>>> dataFromDbForProcedure = await this.adminService.StartAdmissionProcedure();
            //var possibleYears = FreeSpotsCenter.GetAllPossibleYears();
            var model =
                new Dictionary<BaseSchoolModel, Dictionary<ClassLanguageType, List<CandidateDashboardStartProcedureViewModel>>>();
            //status message KLASIRANETO E IZVYRSHENO???
            return this.View(dataFromDbForProcedure);
        }


    }
}
