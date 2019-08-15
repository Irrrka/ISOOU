namespace ISOOU.Web.ViewModels.Schools
{
    using AutoMapper;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditSchoolInputModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SchoolServiceModel, EditSchoolInputModel>()
                .ForMember(destination => destination.DistrictName,
                            opts => opts.MapFrom(origin => origin.District.Name));

            configuration
                .CreateMap<EditSchoolInputModel, SchoolServiceModel>()
                .ForMember(destination => destination.District,
                            opts => opts.MapFrom(origin => new DistrictServiceModel { Name = origin.DistrictName }));

        }
    }

}
