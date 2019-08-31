namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System.Collections.Generic;

    public class CandidateServiceModel : IMapFrom<Candidate>, IMapTo<Candidate>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string UCN { get; set; }

        public string UserId { get; set; }

        public SystemUserServiceModel User { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public bool Immunization { get; set; }

        public CandidateStatus Status { get; set; } = CandidateStatus.NotAdmitted;

        public int MotherId { get; set; }

        public ParentServiceModel Mother { get; set; }

        public int FatherId { get; set; }

        public ParentServiceModel Father { get; set; }

        public int BasicScores { get; set; }

        public ICollection<CandidateApplicationServiceModel> Applications { get; set; }

        public ICollection<CriteriaForCandidateServiceModel> Criterias { get; set; }

        public ICollection<DocumentSubmissionServiceModel> Documents { get; set; }

    }
}
