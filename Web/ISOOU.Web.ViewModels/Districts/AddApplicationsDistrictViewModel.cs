namespace ISOOU.Web.ViewModels.Districts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class AddApplicationsDistrictViewModel : IMapFrom<District>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
