namespace ISOOU.Web.Areas.Administration.Controllers
{
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Data;
    using ISOOU.Web.Areas.Administration.ViewModels.Dashboard;
    using ISOOU.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ISOOU.Data.Models;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IAdmissionProceduresService admissionProceduresService;
        private readonly ISchoolsService schoolsService;

        public DashboardController(ISettingsService settingsService, 
            IAdmissionProceduresService admissionProceduresService,
            ISchoolsService schoolsService)
        {
            this.settingsService = settingsService;
            this.admissionProceduresService = admissionProceduresService;
            this.schoolsService = schoolsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        public async Task<IActionResult> StartAdmissionProcedure()
        {
            IEnumerable<AllSchoolsViewModel> schools = await this.schoolsService.GetAllSchoolsAsync();
            var possibleYears = FreeSpotsCenter.GetAllPossibleYears();

            foreach (var school in schools)
            {
                await this.admissionProceduresService.StartProcedure();
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult StartAdmissionProcedure(StartAdmissionProcedureInputModel model)
        {

            return this.Redirect("Index");
        }
    }
}
