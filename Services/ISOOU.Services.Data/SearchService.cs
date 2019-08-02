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

    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly ISchoolsService schoolsService;
        private readonly IRepository<School> schoolRepository;

        public SearchService(
            ISchoolsService schoolsService,
            IRepository<School> schoolRepository)
        {
            this.schoolsService = schoolsService;
            this.schoolRepository = schoolRepository;
        }

        public async Task<SearchFreeSpotsResultViewModel> GetSearchResultAsync(string district, int year)
        {
            IEnumerable<SchoolClassViewModel> schoolClassesAndFreeSpotsByDistrict = await this.schoolsService
                .GetAllSchoolsByDistrictNameWithClassesAndFreeSpots(district);

            if (schoolClassesAndFreeSpotsByDistrict == null)
            {
                throw new NullReferenceException();
            }

            var searchFreeSpotsResultViewModel = new SearchFreeSpotsResultViewModel();

            foreach (var school in schoolClassesAndFreeSpotsByDistrict)
            {
                var schoolIdName = new BaseSchoolModel { Id = school.Id, Name = school.Name };
                searchFreeSpotsResultViewModel.Result
                    .Add(schoolIdName, new Dictionary<string, int>());
                foreach (var classLang in school.SchoolClassFreeSpotsOfSchoolClassLanguageType)
                {
                    searchFreeSpotsResultViewModel.Result[schoolIdName].Add(classLang.Key, classLang.Value);
                }
            }

            searchFreeSpotsResultViewModel.DistrictName = district;
            searchFreeSpotsResultViewModel.YearOfBirth = year;

            return searchFreeSpotsResultViewModel;
        }

        //public async Task<Dictionary<string, int>> GetFreeSpotsClassesBySchool(int schoolId)
        //{
        //    School school = await this.schoolRepository
        //        .All()
        //        .Where(c => c.Id == schoolId)
        //        .FirstOrDefaultAsync();

        //    if (school == null)
        //    {
        //        throw new NullReferenceException();
        //    }

        //    var freeSpotsByClasses = new Dictionary<string, int>();

        //    foreach (var schoolClass in school.SchoolClasses)
        //    {
        //    freeSpotsByClasses.Add(
        //        schoolClass.Class.LanguageType.ToString(),
        //        schoolClass.Class.FreeSpots);
        //    }

        //    return freeSpotsByClasses;
        //}
    }
}
