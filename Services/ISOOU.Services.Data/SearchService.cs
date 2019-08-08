namespace ISOOU.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Search;

    public class SearchService : ISearchService
    {
        private readonly ISchoolsService schoolsService;

        public SearchService(
            ISchoolsService schoolsService)
        {
            this.schoolsService = schoolsService;
        }

        public async Task<SearchFreeSpotsResultViewModel> GetSearchResult(int districtId, int year)
        {
            IQueryable<SchoolServiceModel> schools = await this.schoolsService
                .GetAllSchoolsByDistrictId(districtId);

            CoreValidator.EnsureNotNull(schools, GlobalConstants.SchoolNotFound);

           
            List<SchoolForSearchResultViewModel> schoolsVM = this.GetSchoolsViewModel(schools);

            var searchFreeSpotsResultViewModel = new SearchFreeSpotsResultViewModel()
            {
                DistrictName = schools.Select(d => d.District.Name).FirstOrDefault(),
                YearOfBirth = year,
                Result = this.GetFreeSpotsBySchool(schoolsVM, year),
            };

            return searchFreeSpotsResultViewModel;
        }

        private Dictionary<SchoolForSearchResultViewModel, int> GetFreeSpotsBySchool(List<SchoolForSearchResultViewModel> schoolsVM, int year)
        {
            int coefByYear = FreeSpotsCenter.CalculateCoeficient(year);

            var result = new Dictionary<SchoolForSearchResultViewModel, int>();

            foreach (var school in schoolsVM)
            {
                if (!result.ContainsKey(school))
                {
                    result.Add(school, school.FreeSpots);
                }

            }

            return result;
        }

        private List<SchoolForSearchResultViewModel> GetSchoolsViewModel(IQueryable<SchoolServiceModel> schools)
        {
            var schoolsVM = new List<SchoolForSearchResultViewModel>();

            foreach (var school in schools)
            {
                var schoolForSearchResultViewModel = new SchoolForSearchResultViewModel()
                {
                    Id = school.Id,
                    Name = school.Name,
                    Address = school.Address,
                    DirectorName = school.DirectorName,
                    DistrictName = school.District.Name,
                    Email = school.Email,
                    PhoneNumber = school.PhoneNumber,
                    UrlOfMap = school.URLOfMap,
                    UrlOfSchool = school.URLOfSchool,
                    FreeSpots = school.FreeSpots,
                };

                schoolsVM.Add(schoolForSearchResultViewModel);
            }

            return schoolsVM;
        }
    }
}
