using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Data.Models
{
    public class CriteriaForCandidate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CriteriaId { get; set; }

        public virtual Criteria Criteria { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public int Sch { get; set; }

    }
}
