namespace ISOOU.Web.Controllers
{
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IUsersService usersService;

        public HomeController(
            IUsersService usersService)
        {
            this.usersService = usersService;
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
     
        //TODO
        public IActionResult News() => this.View();
       
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
