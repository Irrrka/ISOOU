namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class EditCandidateInputModel : IMapFrom<Candidate>
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

        public int YearOfBirth { get; set; }

        public int UCN { get; set; }

        [Display(Name = "Име на посещавана ДГ")]
        public string KinderGarten { get; set; }

        [Display(Name = " СОП ")]
        public string SEN { get; set; }

        [Display(Name = " Хронично заболяване ")]
        public string Desease { get; set; }
    }
}