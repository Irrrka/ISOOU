using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class CalculateScoresByCriteriaOnCandidateViewModel
    {
        public CalculateScoresByCriteriaOnCandidateViewModel()
        {
            this.ScoresByCrieria = new Dictionary<string, int>();
        }

        public string ParentPermanentCity { get; set; }

        public string ParentCurrentCity { get; set; }

        public string ParentPermanentDistrictName { get; set; }

        public string ParentCurrentDistrictName { get; set; }

        public string ParentWorkDistrictName { get; set; }

        public string MotherFullName { get; set; }

        public string FatherFullName { get; set; }

        public Dictionary<string, int> ScoresByCrieria { get; set; }

    }
}