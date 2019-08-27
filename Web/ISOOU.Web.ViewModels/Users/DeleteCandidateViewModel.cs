namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class DeleteCandidateViewModel : IMapFrom<CandidateServiceModel>, IHaveCustomMappings
    {
        public int CandidateId { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "ЕГН")]
        public string UCN { get; set; }

        [Display(Name = "Година на раждане")]
        public int YearOfBirth { get; set; }

        public string MotherFullName { get; set; }

        public string FatherFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                 .CreateMap<CandidateServiceModel, CreateCandidateInputModel>()
                 .ForMember(
                    destination => destination.MotherFullName,
                    opts => opts.MapFrom(origin => origin.Mother.FullName))
                 .ForMember(
                        destination => destination.FatherFullName,
                        opts => opts.MapFrom(origin => origin.Father.FullName));

        }
    }
}