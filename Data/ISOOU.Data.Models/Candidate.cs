namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Candidate : Person
    {
        private string uniqueNumber;

        public Candidate()
            : base()
        {
            this.CandidateSchoolClasses = new HashSet<CandidateSchoolClass>();
            this.Scores = new List<Criteria>();
        }

        public string UniqueNumber => this.GetUniqueNumberFromUCN();

        [Required]
        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public virtual Parent Mother { get; set; }

        public virtual Parent Father { get; set; }

        public virtual ICollection<CandidateSchoolClass> CandidateSchoolClasses { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public ICollection<Criteria> Scores { get; set; }

        private string GetUniqueNumberFromUCN()
        {
            this.uniqueNumber =
            this.UCN.Substring(0, 2).Insert(0, "Y")
            + this.UCN.Substring(2, 2).Insert(2, "M")
            + this.UCN.Substring(4, 2).Insert(4, "D");
            return this.uniqueNumber;
        }
    }
}
