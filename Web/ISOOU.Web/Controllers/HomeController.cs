namespace ISOOU.Web.Controllers
{
    using ISOOU.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class HomeController : BaseController
    {

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        //TODO
        public IActionResult News()
        {
            return this.View();
        }

        //TODO
        public IActionResult Calendar()
        {
            return this.View();
        }

        public IActionResult Helper()
        {
            return this.View();
        }

        public IActionResult Laws()
        {
            return this.View();
        }

        //TODO calendar
        public IActionResult ContactForm()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactForm(ContactFormInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            //    await this.usersService.CreateMessage(model);
            //    model.StatusMessage = "Благодарим за обратната връзка!";
            return this.Redirect("ContactForm");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
