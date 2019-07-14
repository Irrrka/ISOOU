namespace ISOOU.Web.Areas.Administration.Controllers
{
    using ISOOU.Services.Data;
    using ISOOU.Web.Areas.Administration.ViewModels.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IAdmissionProceduresService admissionProceduresService;

        public DashboardController(ISettingsService settingsService, IAdmissionProceduresService admissionProceduresService)
        {
            this.settingsService = settingsService;
            this.admissionProceduresService = admissionProceduresService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
        public ActionResult StartAdmissionProcedureViewModel()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult StartAdmissionProcedureViewModel(StartAdmissionProcedureInputModel model)
        {

            return this.Redirect("Index");
        }
    }
}
