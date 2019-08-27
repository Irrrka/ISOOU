namespace ISOOU.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Home;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IQuestionsService questionsService;
        private readonly ISchoolsService schoolsService;
        private readonly IAdminService adminService;
        private readonly UserManager<SystemUser> userManager;

        public HomeController(
            IQuestionsService questionsService,
            ISchoolsService schoolsService,
            IAdminService adminService,
            UserManager<SystemUser> userManager)
        {
            this.questionsService = questionsService;
            this.schoolsService = schoolsService;
            this.adminService = adminService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    return this.Redirect("/Administration/Dashboard/Index");
                }
                else if (this.User.IsInRole(GlobalConstants.DirectorRoleName))
                {
                    return this.Redirect("/Directors/Home/Index");
                }
                else if (this.User.IsInRole(GlobalConstants.UserRoleName))
                {
                    return this.Redirect("/Users/Home/Index");
                }
            }

            return this.View();
        }

        public IActionResult Privacy() => this.View();

        public IActionResult News()
        {
            SchoolsAdmissionProcedureResultViewModel model =
                new SchoolsAdmissionProcedureResultViewModel();

            model.Status = this.adminService.GetProcedureStatus();
            return this.View(model);
        }

        //TODO
        //public IActionResult Calendar() => this.View();

        public IActionResult Helper() => this.View();

        public IActionResult Laws() => this.View();

        //TODO add map
        public IActionResult ContactForm() => this.View();

        [HttpPost]
        public async Task<IActionResult> ContactForm(ContactFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            QuestionServiceModel model = input.To<QuestionServiceModel>();
            ClaimsPrincipal userIdentity = this.User;
            if (input.UserEmail == userIdentity.Identity.Name)
            {
                model.SystemUserId = this.userManager.GetUserId(userIdentity);
            }

            await this.questionsService.CreateMessage(userIdentity, model);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Admitted()
        {
            var schools = this.schoolsService
                .GetAllSchools()
                .To<AdmitedCandidatesViewModel>()
                .ToList();

            return this.View(schools);
        }

        [HttpGet]
        [Authorize]
        public IActionResult NotAdmitted()
        {
            var schools = this.schoolsService
               .GetAllSchools()
               .To<NotAdmitedCandidatesViewModel>()
               .ToList();

            return this.View(schools);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
