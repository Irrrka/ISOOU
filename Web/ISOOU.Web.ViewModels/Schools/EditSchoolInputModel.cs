using AutoMapper;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOOU.Web.ViewModels.Schools
{
    public class EditSchoolInputModel : IMapFrom<SchoolServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirectorName { get; set; }

        public string Address { get; set; }

        public string DistrictName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public string SchoolClassClassProfile { get; set; }

        public int SchoolClassClassInitialFreeSpots { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SchoolServiceModel, EditSchoolInputModel>()
                .ForMember(
                    d => d.SchoolClassClassProfile,
                    opts => opts.MapFrom(origin => origin.SchoolClasses.Select(c=>c.Class.Profile.Name)));
            configuration.CreateMap<SchoolServiceModel, EditSchoolInputModel>()
                .ForMember(
                    d => d.SchoolClassClassInitialFreeSpots,
                    opts => opts.MapFrom(origin => origin.SchoolClasses.Select(c => c.Class.InitialFreeSpots)));
        }
    }
}
