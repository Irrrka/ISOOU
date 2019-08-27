namespace ISOOU.Web.ViewModels
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class AllSchoolsViewModel : IMapFrom<SchoolServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
