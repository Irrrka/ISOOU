namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System;

    public class AdmissionProcedureServiceModel : IMapFrom<AdmissionProcedure>, IMapTo<AdmissionProcedure>
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string RankingDate { get; set; }

        public string StartApplyDocuments { get; set; }

        public string EndApplyDocuments { get; set; }

        public string StartEnrollment { get; set; }

        public string EndEnrollment { get; set; }

        public AdmissionProcedureStatus Status { get; set; } 

        public int SchoolId { get; set; }

        public SchoolServiceModel School { get; set; }
    }
}
