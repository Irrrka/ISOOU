namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class ViewDocumentViewModel : IMapFrom<DocumentServiceModel>, IMapTo<DocumentServiceModel>
    {
        public int CandidateId { get; set; }

        public string CandidateFullName { get; set; }

        public string Url { get; set; }
    }
}
