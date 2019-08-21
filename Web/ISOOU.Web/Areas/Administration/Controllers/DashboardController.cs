namespace ISOOU.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Home;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
            ContactFormViewModel model = new ContactFormViewModel();

            QuestionServiceModel message = await this.adminService.ReadLastMessage();
            if (message != null)
            {
                model = message.To<ContactFormViewModel>();
            }

            //model.UserEmail = message.SystemUser.Email;
            model.AdmissionProcedureStatus = this.adminService.GetProcedureStatus();

            return this.View(model);
        }

        [HttpGet]
        public ActionResult CreateDirector()
        {
            var allSchools = this.schoolsService.GetAllSchools();
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
            var dataFromProcedure = await this.adminService.AdmissionProcedure();

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<ActionResult> Revert()
        {
            await this.adminService.RevertAdmissionProcedure();
            return this.Redirect("/");
        }


    }
}
