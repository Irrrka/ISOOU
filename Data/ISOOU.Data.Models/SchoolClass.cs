using ISOOU.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISOOU.Data.Models
{
    public class SchoolClass
    {
        public SchoolClass()
        {
            this.CandidateClasses = new HashSet<CandidateSchoolClass>();
        }

        public int Id { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        public virtual ICollection<CandidateSchoolClass> CandidateClasses { get; set; }
    }
}