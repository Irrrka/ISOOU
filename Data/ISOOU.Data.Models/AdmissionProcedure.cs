namespace ISOOU.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ISOOU.Data.Common.Models;

    public class AdmissionProcedure : BaseModel<int>
    {
        public int Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RankingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartApplyDocuments { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndApplyDocuments { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartEnrollment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndEnrollment { get; set; }

        public AdmissionProcedureStatus Status { get; set; } = AdmissionProcedureStatus.Finished;

        public int SchoolID { get; set; }

        public virtual School School { get; set; }
    }
}
