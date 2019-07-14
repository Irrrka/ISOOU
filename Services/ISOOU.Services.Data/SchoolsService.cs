namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ISOOU.Data;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.ViewModels;

    public class SchoolsService : ISchoolsService
    {
        private readonly IRepository<School> repository;
        private readonly IDistrictsService districtsService;

        public SchoolsService(IRepository<School> repository, IDistrictsService districtsService)
        {
            this.repository = repository;
            this.districtsService = districtsService;
        }

        public IEnumerable<TSchoolViewModel> GetAllSchoolsByDistrict<TSchoolViewModel>(int value)
        {
            DistrictViewModel currDistrict = this.districtsService
                                                .GetDistrictByValue<DistrictViewModel>(value);
            var schools = this.repository.All()
                .Where(d => d.Address.District.Name == currDistrict.Name)
                .To<TSchoolViewModel>()
                .ToList();

            return schools;
        }

        public List<SystemUser> GetAllAdmittedCandidates()
        {
            //List<SystemUser> admittedCandidatesFromDb = this.context
            //    .Candidates
            //    .Where(c => c.Status.ToString() == nameof(CandidateStatus.Admitted))
            //    .OrderBy(s => s.Schools
            //    .OrderBy(a => a.Address.District)
            //    .ThenBy(n => n.Ref)).ToList();

            return null;
        }

        public List<SystemUser> GetAllNotAdmittedCandidates()
        {
            return null;
        }
    }
}
