using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class ScoresByCriteriasOnCandidateViewModel
    {
        public ScoresByCriteriasOnCandidateViewModel()
        {
            this.ScoresByCriteria = new List<ScoreByCriteriaOnCandidateViewModel>();
        }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public List<ScoreByCriteriaOnCandidateViewModel> ScoresByCriteria { get; set; }
    }
}