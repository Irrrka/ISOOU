using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class CalculateScoresByCriteriaOnCandidateViewModel
    {

        public string ParentPermanentCity { get; set; }

        public string ParentCurrentCity { get; set; }

        public string ParentPermanentDistrictName { get; set; }

        public string ParentCurrentDistrictName { get; set; }

        public string ParentWorkDistrictName { get; set; }

        public string MotherFullName { get; set; }

        public string FatherFullName { get; set; }

    }
}