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
            this.Scores = new List<Criteria>();
        }

        [Required]
        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public virtual Parent Mother { get; set; }

        public virtual Parent Father { get; set; }

        public virtual ICollection<SchoolCandidate> SchoolCandidates { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public ICollection<Criteria> Scores { get; set; }

    }
}
