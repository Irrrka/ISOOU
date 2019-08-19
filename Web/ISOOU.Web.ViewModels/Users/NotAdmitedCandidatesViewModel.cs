namespace ISOOU.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class NotAdmitedCandidatesViewModel : IMapFrom<SchoolServiceModel>, IHaveCustomMappings
    {
        public NotAdmitedCandidatesViewModel()
        {
            this.NotAdmittedCandidates = new List<string>();
        }

        public string Name { get; set; }

        public List<string> NotAdmittedCandidates { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration
            //  .CreateMap<SchoolServiceModel, AdmitedCandidatesViewModel>()
            //    .ForMember(
            //       destination => destination.AdmittedCandidatesCandidateFullNameUCN,
            //       opts => opts.MapFrom(origin => origin.AdmittedCandidates
            //       .Select(x => x.Candidate.FullName + " " + x.Candidate.UCN)));
        }
    }
}
