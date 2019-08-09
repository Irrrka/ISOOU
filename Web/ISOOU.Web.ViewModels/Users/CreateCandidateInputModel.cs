﻿namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using ISOOU.Common;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class CreateCandidateInputModel : IMapTo<CandidateServiceModel>, IMapFrom<CandidateServiceModel>
    {
        public int Id { get; set; }

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
        public bool SEN { get; set; }

        [Display(Name = " Хронично заболяване ")]
        public bool Desease { get; set; }

        public string UserName { get; set; }

        public int MotherId { get; set; }

        public string MotherFullName { get; set; }

        public int FatherId { get; set; }

        public string FatherFullName { get; set; }

    }
}