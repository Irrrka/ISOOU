namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class SchoolClassForSearchResultViewModel : IMapFrom<SchoolClassServiceModel>
    {
        public int Id { get; set; }

        public int SchoolId { get; set; }

        public int ClassId { get; set; }

        public string SchoolName { get; set; }

        public string ClassProfileName { get; set; }

        public int ClassFreeSpots { get; set; }

        //private Dictionary<string, int> FillDictionary(List<string> languageTypes, List<int> freeSpots)
        //{
        //    var result = new Dictionary<string, int>();
        //    for (int i = 0; i < languageTypes.Count; i++)
        //    {
        //        result.Add(languageTypes[i], freeSpots[i]);
        //    }

        //    return result;
        //}
    }
}
