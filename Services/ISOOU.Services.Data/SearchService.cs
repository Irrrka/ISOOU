namespace ISOOU.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Search;
    using Microsoft.EntityFrameworkCore;

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

            List<string> allClassProfiles = this.schoolsService.GetAllClassProfiles()
                                                                        .Select(x => x.Name)
                                                                        .OrderBy(name => name).ToList();

            List<SchoolForSearchResultViewModel> schoolsVM = this.GetSchoolsViewModel(schools);

            var searchFreeSpotsResultViewModel = new SearchFreeSpotsResultViewModel()
            {
                DistrictName = schools.Select(d => d.District.Name).FirstOrDefault(),
                YearOfBirth = year,
                Result = this.GetFreeSpotsBySchoolClassesAndBySchool(schoolsVM, allClassProfiles, year),
            };

            return searchFreeSpotsResultViewModel;
        }

        private Dictionary<SchoolForSearchResultViewModel, List<int>> GetFreeSpotsBySchoolClassesAndBySchool(List<SchoolForSearchResultViewModel> schoolsVM, List<string> allClassProfiles, int year)
        {
            int coefByYear = FreeSpotsCenter.CalculateCoeficient(year);

            var result = new Dictionary<SchoolForSearchResultViewModel, List<int>>();

            foreach (var school in schoolsVM)
            {
                if (!result.ContainsKey(school))
                {
                    result.Add(school, new List<int>());
                }

                foreach (var classProfile in allClassProfiles)
                {
                    SchoolClassForSearchResultViewModel exist =
                        school.SchoolClasses.FirstOrDefault(c => c.ClassProfileName == classProfile);
                    if (exist != null)
                    {
                        result[school].Add(exist.ClassFreeSpots * coefByYear);
                    }
                    else
                    {
                        result[school].Add(0);
                    }
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
                    SchoolClasses = new List<SchoolClassForSearchResultViewModel>(),
                };

                foreach (SchoolClassServiceModel schoolClass in school.SchoolClasses)
                {
                    SchoolClassForSearchResultViewModel schoolClassViewModel =
                        new SchoolClassForSearchResultViewModel();

                    schoolClassViewModel.ClassProfileName = schoolClass.Class.Profile.Name;
                    //TODO Coef?
                    schoolClassViewModel.ClassFreeSpots = schoolClass.Class.InitialFreeSpots;
                    schoolClassViewModel.ClassId = schoolClass.ClassId;
                    schoolClassViewModel.SchoolName = school.Name;
                    schoolClassViewModel.SchoolId = school.Id;

                    schoolForSearchResultViewModel.SchoolClasses.Add(schoolClassViewModel);
                }

                schoolsVM.Add(schoolForSearchResultViewModel);
            }

            return schoolsVM;
        }
    }
}
