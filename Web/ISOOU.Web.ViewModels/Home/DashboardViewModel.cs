using AutoMapper;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System;

namespace ISOOU.Web.ViewModels.Home
{
    public class DashboardViewModel : IMapTo<QuestionServiceModel>, IMapFrom<QuestionServiceModel>, IMapTo<AdmissionProcedureServiceModel>, IMapFrom<AdmissionProcedureServiceModel>, IHaveCustomMappings
    {
        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string Status { get; set; }

        public string RankingDate { get; set; }

        public string StartApplyDocuments { get; set; }

        public string EndApplyDocuments { get; set; }

        public string StartEnrollment { get; set; }

        public string EndEnrollment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<QuestionServiceModel, DashboardViewModel>()
              .ForMember(
                   destination => destination.UserEmail,
                   opts => opts.MapFrom(origin => origin.SystemUser.Email));
        }
    }
}
