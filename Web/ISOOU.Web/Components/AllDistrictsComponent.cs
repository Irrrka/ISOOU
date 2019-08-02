namespace ISOOU.Web.Components
{
    using ISOOU.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class AllDistrictsComponent : ViewComponent
    {
        private readonly IDistrictsService districtService;

        public AllDistrictsComponent(IDistrictsService districtService)
        {
            this.districtService = districtService;
        }

        public IViewComponentResult InvokeAsync()
        {
            var allDistricts = this.districtService.GetAllDistrictsAsync();
            return this.View(allDistricts);
        }
    }
}
