namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System;

    public class AdmissionProcedureServiceModel : IMapFrom<AdmissionProcedure>, IMapTo<AdmissionProcedure>
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime? RankingDate { get; set; }

        public DateTime? StartApplyDocuments { get; set; }

        public DateTime? EndApplyDocuments { get; set; }

        public DateTime? StartEnrollment { get; set; }

        public DateTime? EndEnrollment { get; set; }

        public AdmissionProcedureStatus Status { get; set; } = AdmissionProcedureStatus.Finished;

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }
    }
}
