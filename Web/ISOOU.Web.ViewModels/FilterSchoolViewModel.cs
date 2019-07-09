namespace ISOOU.Web.ViewModels
{
    public class FilterSchoolViewModel
    {
        public string District { get; set; }

        public string Name { get; set; }

        public int FreePlaces { get; set; }

        public string Address { get; set; }

        public string UrlOfSchool { get; set; }

        public string UrlOfMap { get; set; }

        public int[] YearsToCandidate { get; set; }
    }
}
