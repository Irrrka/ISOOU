using ISOOU.Common;
using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class CreateCandidateInputModel : IMapFrom<CandidateServiceModel>, IMapTo<CandidateServiceModel>
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "ЕГН")]
        public string UCN { get; set; }

        [Required]
        [Display(Name = "Година на раждане")]
        [CorrectYearAttribute]
        [EqualUCNandYearOfBirthAttribute("UCN")]
        public int YearOfBirth { get; set; }

        [Display(Name = "Име на посещавана ДГ")]
        public string KinderGarten { get; set; }

        [Display(Name = " СОП ")]
        public string SEN { get; set; }

        [Display(Name = " Хронично заболяване ")]
        public string Desease { get; set; }
    }
}