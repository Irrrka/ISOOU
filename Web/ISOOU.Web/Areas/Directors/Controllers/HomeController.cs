namespace ISOOU.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : DirectorsController
    {
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<SystemUser> userManager;

        public HomeController(
            ISchoolsService schoolsService,
            UserManager<SystemUser> userManager)
        {
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult CreateClassProfile()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateClassProfile(string name)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.schoolsService.CreateClassProfile(name);

            return this.Redirect("/");
        }

        [HttpGet]
        public ActionResult EditSchool([FromQuery(Name = "name")] string name)
        {
            IQueryable<SchoolServiceModel> allSchools = this.schoolsService.GetAllSchools();
            this.ViewData["Schools"] = allSchools.To<BaseSchoolModel>().ToList();

            IQueryable<ClassProfileServiceModel> allClassProfiles = this.schoolsService.GetAllClassProfiles();
            this.ViewData["ClassProfiles"] = allClassProfiles.To<ClassProfileForEditSchoolViewModel>().ToList();

            if (name == null)
            {
                return this.View();
            }
            else
            {
                var schoolToEdit = allSchools
                                    .FirstOrDefault(x => x.Name== name)
                                    .To<EditSchoolViewModel>();
                return this.View(schoolToEdit);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditSchool(EditSchoolInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string classProfileName = model.SchoolClassClassProfile;
            int classInitialFreeSpots = model.SchoolClassClassInitialFreeSpots;

            SchoolServiceModel schoolToEdit = model.To<SchoolServiceModel>();

            ClassProfileServiceModel profile = this.schoolsService.GetAllClassProfiles()
                                        .FirstOrDefault(x => x.Name == classProfileName);
            ClassServiceModel @class = new ClassServiceModel
            {
                Profile = profile,
                InitialFreeSpots = classInitialFreeSpots,
            };

            await this.schoolsService.EditSchool(@class,schoolToEdit);

            return this.Redirect("/");
        }



    }
}
