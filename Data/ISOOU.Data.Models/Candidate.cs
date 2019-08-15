namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Candidate : Person
    {

        public Candidate()
            : base()
        {
            this.SchoolCandidates = new HashSet<SchoolCandidate>();
           // this.CandidateParents = new HashSet<CandidateParent>();
            this.Criterias = new HashSet<CriteriaForCandidate>();
            this.Documents = new HashSet<DocumentSubmission>();
        }

        [Required]
        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public bool Immunization { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public int MotherId { get; set; }

        public Parent Mother { get; set; }

        public int FatherId { get; set; }

        public Parent Father { get; set; }

        //public virtual ICollection<CandidateParent> CandidateParents { get; set; }

        public virtual ICollection<SchoolCandidate> SchoolCandidates { get; set; }

        public virtual ICollection<CriteriaForCandidate> Criterias { get; set; }

        public ICollection<DocumentSubmission> Documents { get; set; }
    }
}
