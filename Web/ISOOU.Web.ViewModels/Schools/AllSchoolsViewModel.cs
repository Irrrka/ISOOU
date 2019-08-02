namespace ISOOU.Web.ViewModels
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class AllSchoolsViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
