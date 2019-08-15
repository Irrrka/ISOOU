using AutoMapper;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;

namespace ISOOU.Web.ViewModels.Schools
{
    public class EditSchoolViewModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public int FreeSpots { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
          
        }
    }
}
