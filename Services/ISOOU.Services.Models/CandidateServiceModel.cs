namespace ISOOU.Services.Models
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using System.Collections.Generic;

    public class CandidateServiceModel : IMapFrom<Candidate>, IMapTo<Candidate>
    {
        public CandidateServiceModel()
        {
           // this.CandidateParents = new List<CandidateParentServiceModel>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

        public string UserId { get; set; }

        public SystemUserServiceModel User { get; set; }

        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

        public bool Immunization { get; set; }

        public int MotherId { get; set; }

        public ParentServiceModel Mother { get; set; }

        public int FatherId { get; set; }

        public ParentServiceModel Father { get; set; }

        public List<SchoolCandidateServiceModel> SchoolCandidates { get; set; }
               
       // public List<CandidateParentServiceModel> CandidateParents { get; set; }

        public CandidateStatus Status { get; set; }

        public List<CriteriaServiceModel> Scores { get; set; }

    }
}
