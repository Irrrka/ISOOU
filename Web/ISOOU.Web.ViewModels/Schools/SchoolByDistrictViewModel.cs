using ISOOU.Services.Mapping;
using ISOOU.Services.Models;

namespace ISOOU.Web.ViewModels
{
    public class SchoolByDistrictViewModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirectorName { get; set; }

        public string Address { get; set; }

        public string DistrictName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UrlOfSchool { get; set; }

        public string UrlOfMap { get; set; }

    }
}
