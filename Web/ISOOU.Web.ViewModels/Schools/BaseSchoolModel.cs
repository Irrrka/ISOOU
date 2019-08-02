namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class BaseSchoolModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
