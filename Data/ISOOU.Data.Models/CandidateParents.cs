using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Data.Models
{
    public class CandidateParents
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }
    }
}
