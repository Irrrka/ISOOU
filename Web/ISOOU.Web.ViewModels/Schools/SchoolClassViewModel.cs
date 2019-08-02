namespace ISOOU.Web.ViewModels.Schools
{
    using AutoMapper;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    using System.Collections.Generic;
    using System.Linq;

    public class SchoolClassViewModel : IMapFrom<Class>, IHaveCustomMappings
    {
        public SchoolClassViewModel()
        {
            this.SchoolClassFreeSpotsOfSchoolClassLanguageType = new Dictionary<string, int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string DistrictName { get; set; }

        public Dictionary<string, int> SchoolClassFreeSpotsOfSchoolClassLanguageType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<School, SchoolClassViewModel>().ForMember(
                x => x.SchoolClassFreeSpotsOfSchoolClassLanguageType,
                opt => opt.MapFrom(d => this.FillDictionary(
                    d.SchoolClasses.Select(lan => lan.Class.Profile.ToString()).ToList(),
                    d.SchoolClasses.Select(fs => fs.Class.FreeSpots).ToList())));
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
