using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;

namespace ISOOU.Services.Models
{
    public class CandidateServiceModel : IMapFrom<Candidate>, IMapTo<Candidate>
    {
        public CandidateServiceModel()
            : base()
        {
            this.SchoolCandidates = new HashSet<SchoolCandidateServiceModel>();
            this.Scores = new List<CriteriaServiceModel>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string UCN { get; set; }

        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public virtual ParentServiceModel Mother { get; set; }

        public virtual ParentServiceModel Father { get; set; }

        public string UserId { get; set; }

        public SystemUserServiceModel User { get; set; }

        public virtual ICollection<SchoolCandidateServiceModel> SchoolCandidates { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public ICollection<CriteriaServiceModel> Scores { get; set; }
    }
}