namespace ISOOU.Web.ViewModels.Districts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class DistrictViewComponentViewModel : IMapFrom<DistrictServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
