namespace ISOOU.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class AdmitedCandidatesViewModel : IMapFrom<SchoolServiceModel>, IHaveCustomMappings
        {
            public AdmitedCandidatesViewModel()
            {
                this.AdmittedCandidates = new List<string>();
            }

            public string Name { get; set; }

            public List<string> AdmittedCandidates { get; set; }

            public void CreateMappings(IProfileExpression configuration)
            {
                configuration
                  .CreateMap<SchoolServiceModel, AdmitedCandidatesViewModel>()
                  .ForMember(
                       destination => destination.AdmittedCandidates,
                       opts => opts.MapFrom(origin => origin.Candidates
                       .Where(c => c.Candidate.Status == Data.Models.CandidateStatus.Admitted)
                       .Select(c => c.Candidate.FullName)));
            }
        }
}
