namespace ISOOU.Web.ViewModels.Schools
{
    using AutoMapper;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    using System.Collections.Generic;
    using System.Linq;

    public class SchoolDetails : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public string DistrictName { get; set; }

        public int YearOfBirth { get; set; }

        public Dictionary<string, int> FreeSpotsBySchoolClass { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<School, SchoolDetails>().ForMember(
                x => x.FreeSpotsBySchoolClass,
                opt => opt.MapFrom(d => this.FillDictionary(
                    d.SchoolClasses.Select(lan => lan.Class.Profile.ToString()).ToList(),
                    d.SchoolClasses.Select(fs => fs.Class.InitialFreeSpots).ToList())));
        }

        private Dictionary<string, int> FillDictionary(List<string> languageTypes, List<int> freeSpots)
        {
            var result = new Dictionary<string, int>();
            for (int i = 0; i < languageTypes.Count; i++)
            {
                result.Add(languageTypes[i], freeSpots[i]);
            }

            return result;
        }
    }
}
