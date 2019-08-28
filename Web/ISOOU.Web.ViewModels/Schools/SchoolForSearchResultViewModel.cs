namespace ISOOU.Web.ViewModels
{
    using System.Collections.Generic;
    using AutoMapper;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Schools;

    public class SchoolForSearchResultViewModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string DistrictName { get; set; }

        public int FreeSpots { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<SchoolServiceModel, SchoolForSearchResultViewModel>()
              .ForMember(
                   destination => destination.DistrictName,
                   opts => opts.MapFrom(origin => origin.District.Name));
        }
    }
}
