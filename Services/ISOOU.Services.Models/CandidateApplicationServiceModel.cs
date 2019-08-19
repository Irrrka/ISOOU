namespace ISOOU.Services.Models
{
    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class CandidateApplicationServiceModel : IMapFrom<CandidateApplication>, IMapTo<CandidateApplication>
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public CandidateServiceModel Candidate { get; set; }

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }

        public int AdditionalScoresForSchool { get; set; }

        public int TotalScoresForSchool { get; set; }
    }
}
