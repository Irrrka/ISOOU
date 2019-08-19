using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Services.Models
{
    public class CriteriaForCandidateServiceModel : IMapFrom<CriteriaForCandidate>, IMapTo<CriteriaForCandidate>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CriteriaId { get; set; }

        public virtual CriteriaServiceModel Criteria { get; set; }

        public int CandidateId { get; set; }

        public virtual CandidateServiceModel Candidate { get; set; }

        public int Sch { get; set; }
    }
}
