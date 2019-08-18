namespace ISOOU.Data.Models
{
    using System.Collections.Generic;

    using ISOOU.Data.Common.Models;

    public class Criteria : BaseModel<int>
    {
        public Criteria()
        {
            this.Candidates = new HashSet<CriteriaForCandidate>();
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Scores { get; set; }

        public virtual ICollection<CriteriaForCandidate> Candidates { get; set; }
    }
}
