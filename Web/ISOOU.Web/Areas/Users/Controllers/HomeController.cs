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
    using ISOOU.Web.Areas.Users.Models;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : UserController
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;

        private bool userHasParents;

        public HomeController(
            UserManager<SystemUser> userManager,
            IParentsService parentsService,
            ICandidatesService candidatesService)
        {
            this.userManager = userManager;
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;

            this.userHasParents = false;
        }

        public async Task<IActionResult> Index()
        {
            var parentsFromService = await this.parentsService.GetParents();
            var parents = await parentsFromService
                .Select(p => new ParentsHomeViewModel
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                })
                .ToListAsync();

            var candidatesFromService = await this.candidatesService.GetCandidates();
            var candidates = await candidatesFromService
                .Select(c => new CandidatesHomeViewModel
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName,
                })
                .ToListAsync();

            var familyHomeViewModel = new FamilyHomeViewModel()
            {
                Parents = parents,
                Candidates = candidates,
            };

            //this.ViewData["Parents"] = parents;
            //this.ViewData["Candidates"] = candidates;

            return this.View(familyHomeViewModel);
        }

    }
}
