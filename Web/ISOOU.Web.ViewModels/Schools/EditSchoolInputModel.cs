namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditSchoolInputModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string DistrictName { get; set; }

        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }
        [Required]
        public int FreeSpots { get; set; }
    }
}
