namespace ISOOU.Web.Controllers
{
    using System.Collections.Generic;
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly ISchoolsService schoolsService;
        private readonly IAdminService adminService;

        public HomeController(
            IUsersService usersService,
            ISchoolsService schoolsService,
            IAdminService adminService)
        {
            this.usersService = usersService;
            this.schoolsService = schoolsService;
            this.adminService = adminService;
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

            QuestionServiceModel model = new QuestionServiceModel();
            var userIdentity = input.UserEmail;
            model = input.To<QuestionServiceModel>();
            await this.usersService.CreateMessage(userIdentity, model);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Admitted()
        {
            //TODO NOTWORKING ANYMORE MAPPING ? OR BROKEN DB!!!
            var schools = this.schoolsService.GetAllSchools().ToList();
            
            //var candidateApp = this.schoolsService.GetSchoolsAndCandidates().ToList();

            List<AdmitedCandidatesViewModel> models =
                new List<AdmitedCandidatesViewModel>();

            foreach (var school in schools)
            {
                var candidates = school.Candidates.Where(s => s.Candidate.Status == CandidateStatus.Admitted);
                var admittedCandidatesNames = candidates.Select(n => n.Candidate.FullName).ToList();
                models.Add(new AdmitedCandidatesViewModel { Name = school.Name, AdmittedCandidates = admittedCandidatesNames});
            }

            return this.View(models);
        }

        [HttpGet]
        [Authorize]
        public IActionResult NotAdmitted()
        {
            var schools = this.schoolsService.GetAllSchools().ToList();

            List<NotAdmitedCandidatesViewModel> models =
                new List<NotAdmitedCandidatesViewModel>();

            foreach (var school in schools)
            {
                var candidates = school.Candidates.Where(s => s.Candidate.Status == CandidateStatus.NotAdmitted);
                var notAdmittedCandidatesNames = candidates.Select(n => n.Candidate.FullName).ToList();
                models.Add(new NotAdmitedCandidatesViewModel { Name = school.Name, NotAdmittedCandidates = notAdmittedCandidatesNames });
            }

            return this.View(models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
