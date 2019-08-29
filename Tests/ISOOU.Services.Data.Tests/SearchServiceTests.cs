namespace ISOOU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.ViewModels.Search;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SearchServiceTests : BaseServiceTests
    {
        private ISearchService SearchServiceMock =>
            this.ServiceProvider.GetRequiredService<ISearchService>();

        private IDistrictsService DistrictsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();

        private ISchoolsService SchoolsServiceMock =>
            this.ServiceProvider.GetRequiredService<ISchoolsService>();


        [Fact]
        public async Task GetSearchResult_WithCorrectInputData_ShouldReturnSearchResult()
        {
            this.SeedTestData(this.DbContext);
            var permanentDistrictId = this.DbContext.Schools.Select(d => d.DistrictId).First();
            var currentDistrictId = this.DbContext.Schools.Select(d => d.DistrictId).Last();
            var districtsIds = new List<int> { permanentDistrictId, currentDistrictId };
            var schools = new List<string>();

            foreach (var id in districtsIds)
            {
                schools.Add(this.DbContext.Schools.FirstOrDefault(d => d.DistrictId == id).Name);
            }

            var actual = await this.SearchServiceMock.GetSearchResult(districtsIds);

            foreach (var data in actual.SchoolsVM)
            {
                Assert.True(
                        schools.Contains(data.Name),
                        "SearchService GetSearchResult() not works properly!");
            }
        }

        [Fact]
        public async Task GetSearchResult_WithNoInputData_ShouldReturnEmptyList()
        {
            this.SeedTestData(this.DbContext);
            this.DbContext.Districts.Add(new District { Id = 1000, Name = "Неизвестен" });
            await this.DbContext.SaveChangesAsync();

            var workDistrictId = 1000;
            var districtsIds = new List<int> { workDistrictId };
            //var schools = new List<string>();

            var actual = await this.SearchServiceMock.GetSearchResult(districtsIds);

            Assert.True(
                    actual.SchoolsVM.Count == 0,
                    "SearchService GetSearchResult() not works properly!");
        }

        [Fact]
        public async Task GetSearchResult_WithRepeatedInputData_ShouldReturnDistinctListt()
        {
            this.SeedTestData(this.DbContext);
            var permanentDistrictId = this.DbContext.Schools.Select(d => d.DistrictId).First();
            var currentDistrictId = this.DbContext.Schools.Select(d => d.DistrictId).First();
            var districtsIds = new List<int> { permanentDistrictId, currentDistrictId };
            var expected = this.DbContext.Schools.Where(d => d.DistrictId == permanentDistrictId).ToList();

            var actual = await this.SearchServiceMock.GetSearchResult(districtsIds);
            //???
            Assert.True(
                    actual.SchoolsVM.Count == expected.Count,
                    "SearchService GetSearchResult() not works properly!");
        }


        private void SeedTestData(ISOOUDbContext context)
        {
            context.Schools.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<School> GetTestData()
        {
            return new List<School>
            {
                new School
                {
                    Name = "107мо",
                    Address = "ул. Хан Крум",
                    DirectorName = "Мария Мария",
                    PhoneNumber = "02/000001",
                    District = new District { Id = 77, Name = "Капана" },
                },
                new School
                {
                    Name = "108мо",
                    Address = "ул. Хан Кру2",
                    DirectorName = "Мария Мария Mariq",
                    PhoneNumber = "02/0011101",
                    District = new District { Id = 88, Name = "Меден Рудник" },
                },
                new School
                {
                    Name = "109то",
                    Address = "ул. Хан Кру22",
                    DirectorName = "Мария Мария Mariq Мария",
                    PhoneNumber = "02/111111",
                    District = new District { Id = 99, Name = "Железен Рудник" },
                },
            };
        }
    }
}