namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class CreateDirectorSchoolModel : IMapFrom<SchoolServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
