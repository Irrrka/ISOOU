namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System;

    public class DocumentServiceModel : IMapFrom<Document>, IMapTo<Document>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int CandidateId { get; set; }

        public CandidateServiceModel Candidate { get; set; }

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }

        public string Url { get; set; }
    }
}
