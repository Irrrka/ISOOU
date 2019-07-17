namespace ISOOU.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult QA()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
