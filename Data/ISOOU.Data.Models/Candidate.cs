namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Candidate : Person
    {

        public Candidate()
            : base()
        {
            this.Applications = new HashSet<CandidateApplication>();
            this.Criterias = new HashSet<CriteriaForCandidate>();
            this.Documents = new HashSet<Document>();
        }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public bool Immunization { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public int? MotherId { get; set; }

        public virtual Parent Mother { get; set; }

        public int? FatherId { get; set; }

        public virtual Parent Father { get; set; }

        public int BasicScores { get; set; }

        public virtual ICollection<CandidateApplication> Applications { get; set; }

        public virtual ICollection<CriteriaForCandidate> Criterias { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
