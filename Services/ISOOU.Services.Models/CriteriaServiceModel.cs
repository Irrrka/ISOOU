namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System.Collections.Generic;

    public class CriteriaServiceModel : IMapFrom<Criteria>, IMapTo<Criteria>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Scores { get; set; }

        public ICollection<CriteriaForCandidate> Candidates { get; set; }
    }
}
