namespace ISOOU.Web.ViewModels.Search
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class SearchDistrictViewModel : IMapFrom<DistrictServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
