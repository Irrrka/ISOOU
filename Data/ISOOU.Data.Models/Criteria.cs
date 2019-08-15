namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;
    using System.Collections.Generic;

    public class Criteria : BaseModel<int>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Scores { get; set; }


        public ICollection<CriteriaForCandidate> Candidates { get; set; }
    }
}
