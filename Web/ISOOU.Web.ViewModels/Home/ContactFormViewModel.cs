using AutoMapper;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;

namespace ISOOU.Web.ViewModels.Home
{
    public class ContactFormViewModel : IMapTo<QuestionServiceModel>, IMapFrom<QuestionServiceModel>, IHaveCustomMappings
    {
        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string AdmissionProcedureStatus { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<QuestionServiceModel, ContactFormViewModel>()
              .ForMember(
                   destination => destination.UserEmail,
                   opts => opts.MapFrom(origin => origin.SystemUser.Email));
        }
    }
}
