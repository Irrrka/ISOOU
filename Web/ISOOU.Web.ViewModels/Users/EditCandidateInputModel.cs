﻿namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class EditCandidateInputModel : IMapFrom<CandidateServiceModel>, IMapTo<CandidateServiceModel>, IHaveCustomMappings
    {
        public int CandidateId { get; set; }

        public string CandidateName{ get; set; }

        [Display(Name = "ЕГН*")]
        public string UCN { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Име*")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Презиме*")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        [Display(Name = "Фамилия*")]
        public string LastName { get; set; }

        [Display(Name = "Име на посещавана ДГ")]
        public string KinderGarten { get; set; }

        [Display(Name = " СОП ")]
        public bool SEN { get; set; }

        [Display(Name = " Хронично заболяване ")]
        public bool Desease { get; set; }

        [Display(Name = " Имунизации")]
        public bool Immunization { get; set; }

        public string MotherFullName { get; set; }

        public string FatherFullName { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                  .CreateMap<CandidateServiceModel, EditCandidateInputModel>()
                  .ForMember(
                       destination => destination.CandidateId,
                       opts => opts.MapFrom(origin => origin.Id));
        }
    }
}