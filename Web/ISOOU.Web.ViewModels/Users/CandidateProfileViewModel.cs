namespace ISOOU.Web.ViewModels.Users
{ 
    using System.Collections.Generic;

    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class CandidateProfileViewModel : IMapFrom<CandidateServiceModel>, IMapTo<CandidateServiceModel>
    {

        public CandidateProfileViewModel()
        {
            this.ScoresByApplications = new Dictionary<string, int>();
        }

        public int CandidateId { get; set; }

        public Dictionary<string, int> ScoresByApplications { get; set; }
    }
}