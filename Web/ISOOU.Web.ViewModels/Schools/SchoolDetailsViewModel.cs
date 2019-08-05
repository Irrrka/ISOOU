namespace ISOOU.Web.ViewModels.Schools
{
    using AutoMapper;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class SchoolDetailsViewModel : IMapFrom<SchoolServiceModel>, IHaveCustomMappings
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

        public Dictionary<string, int> FreeSpotsBySchoolClass { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SchoolServiceModel, SchoolDetailsViewModel>().ForMember(
                x => x.FreeSpotsBySchoolClass,
                opt => opt.MapFrom(d => this.FillDictionary(
                    d.SchoolClasses.Select(lan => lan.Class.Profile.Name).ToList(),
                    d.SchoolClasses.Select(fs => fs.Class.InitialFreeSpots).ToList())));
        }

        private Dictionary<string, int> FillDictionary(List<string> classProfiles, List<int> freeSpots)
        {
            var result = new Dictionary<string, int>();
            for (int i = 0; i < classProfiles.Count; i++)
            {
                result.Add(classProfiles[i], freeSpots[i]);
            }

            return result;
        }
    }
}
