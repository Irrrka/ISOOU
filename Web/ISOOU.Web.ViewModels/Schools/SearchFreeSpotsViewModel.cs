namespace ISOOU.Web.ViewModels
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class SearchFreeSpotsViewModel : IMapFrom<Candidate>, IMapFrom<District>
    {
        public int Id { get; set; }

        public int YearOfBirth { get; set; }

        public string District { get; set; }
    }
}
