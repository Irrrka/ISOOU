namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ISOOU.Data.Common.Models;

    public class AdmissionProcedure : BaseModel<int>
    {
        public AdmissionProcedure()
        {
            this.ParticipatedCandidates = new List<CandidateApplication>();
        }

        public int Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime RankingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartApplyDocuments { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndApplyDocuments { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartEnrollment { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndEnrollment { get; set; }

        public AdmissionProcedureStatus Status { get; set; } = AdmissionProcedureStatus.NotFinished;

        public List<CandidateApplication> ParticipatedCandidates { get; set; }
    }
}
