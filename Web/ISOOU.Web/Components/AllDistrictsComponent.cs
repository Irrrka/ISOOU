namespace ISOOU.Web.Components
{
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.ViewModels.Districts;
    using Microsoft.AspNetCore.Mvc;


    public class AllDistrictsComponent : ViewComponent
    {
        private readonly IDistrictsService districtService;

        public AllDistrictsComponent(IDistrictsService districtService)
        {
            this.districtService = districtService;
        }

        public IViewComponentResult Invoke()
        {
            var allDistricts = this.districtService.GetAllDistricts().To<DistrictViewComponentViewModel>();
            return this.View(allDistricts);
        }
    }
}
