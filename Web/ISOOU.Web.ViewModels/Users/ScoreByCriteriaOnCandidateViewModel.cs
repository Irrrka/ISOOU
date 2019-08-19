using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class ScoreByCriteriaOnCandidateViewModel
    {
        public string CriteriaDisplayName { get; set; }

        public int CriteriaScores { get; set; }

        public string SchoolName { get; set; }
    }
}