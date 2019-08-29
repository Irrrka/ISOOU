namespace ISOOU.Web.Areas.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.Areas.Users.Controllers;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using ISOOU.Web.ViewModels.Schools;

    public class HomeController : UserController
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;
        private readonly ISchoolsService schoolsService;

        private bool userHasParents;

        public HomeController(
            UserManager<SystemUser> userManager,
            IParentsService parentsService,
            ICandidatesService candidatesService,
            ISchoolsService schoolsService)
        {
            this.userManager = userManager;
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
            this.schoolsService = schoolsService;

            this.userHasParents = false;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClaimsPrincipal userIdentity = this.User;

            var parents = this.parentsService.GetParents(userIdentity)
                .Select(p => new ParentsHomeViewModel
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                })
                .ToList();

            var candidates = this.candidatesService.GetCandidates()
                 .Where(x => x.User.UserName == this.User.Identity.Name)
                .Select(c => new CandidatesHomeViewModel
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName,
                })
                .ToList();

            var familyHomeViewModel = new FamilyHomeViewModel()
            {
                Parents = parents,
                Candidates = candidates,
            };

            if (familyHomeViewModel.Parents.Count > 0 || familyHomeViewModel.Candidates.Count > 0)
            {
                return this.View(familyHomeViewModel);
            }

            return this.View();
        }

    }
}
