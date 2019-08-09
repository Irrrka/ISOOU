namespace ISOOU.Data.Models
{
    using ISOOU.Data.Models.Enums;

    using System.Collections.Generic;

    public class Parent : Person
    {
        public Parent() : base()
        {
            this.CandidateParents = new HashSet<CandidateParent>();
        }

        public ParentRole Role { get; set; }

        public string WorkName { get; set; }

        public virtual District WorkDistrict { get; set; }

        public virtual ICollection<CandidateParent> CandidateParents { get; set; }

    }
}
