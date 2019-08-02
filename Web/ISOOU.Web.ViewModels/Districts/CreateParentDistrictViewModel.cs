namespace ISOOU.Web.ViewModels.Districts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class CreateParentDistrictViewModel : IMapFrom<District>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
