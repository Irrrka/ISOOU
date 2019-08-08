namespace ISOOU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Services.Mapping;
    using ISOOU.Data.Models;
    using Xunit;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using ISOOU.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class SchoolsServiceTests : BaseServiceTests
    {
        private ISchoolsService SchoolsServiceMock =>
            this.ServiceProvider.GetRequiredService<ISchoolsService>();
        private IDistrictsService DistrictsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();


        [Fact]
        public async Task GetAllSchools_WithCorrectInputData_ShouldReturnAllSchools()
        {
            this.SeedTestData(this.DbContext);

            List<SchoolServiceModel> expected = this.DbContext.Schools.To<SchoolServiceModel>().ToList();
            List<SchoolServiceModel> actual = await this.SchoolsServiceMock.GetAllSchools().ToListAsync();

            foreach (var data in actual)
            {
                Assert.True(
                        expected.Any(n => n.Name == data.Name),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.Address == data.Address),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.PhoneNumber == data.PhoneNumber),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.DirectorName == data.DirectorName),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.District.Name == data.District.Name),
                        "SchoolsService GetAllSchools() not works properly!");
            }
        }

        [Fact]
        public async Task GetAllSchools_WithEmptyData_ShouldReturnEmptyList()
        {
            List<SchoolServiceModel> expected = new List<SchoolServiceModel>();
            List<SchoolServiceModel> actual = await this.SchoolsServiceMock.GetAllSchools().ToListAsync();

            Assert.True(
                        actual.Count == 0,
                        "SchoolsService GetAllSchools() not works properly!");
        }

        [Fact]
        public async Task GetSchoolDetails_WithCorrectInputData_ShouldReturnSchoolDetails()
        {
            this.SeedTestData(this.DbContext);

            SchoolServiceModel expected = this.DbContext.Schools.To<SchoolServiceModel>().First();

            SchoolServiceModel actual = await this.SchoolsServiceMock.GetSchoolDetailsById(expected.Id);

            Assert.True(
                        expected.Name == actual.Name,
                        "SchoolsService GetSchoolDetailsById() not works properly!");
            Assert.True(
                    expected.PhoneNumber == actual.PhoneNumber,
                    "SchoolsService GetSchoolDetailsById() not works properly!");
            Assert.True(
                       expected.District.Name == actual.District.Name,
                       "SchoolsService GetSchoolDetailsById() not works properly!");
            Assert.True(
                       expected.DirectorName == actual.DirectorName,
                       "SchoolsService GetSchoolDetailsById() not works properly!");
            Assert.True(
                       expected.Address == actual.Address,
                       "SchoolsService GetSchoolDetailsById() not works properly!");
        }

        [Fact]
        public async Task GetSchoolDetails_WithInCorrectInputData_ShouldThrowException()
        {
            this.SeedTestData(this.DbContext);

            int id = 100;
            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.SchoolsServiceMock.GetSchoolDetailsById(id));
        }

        [Fact]
        public async Task GetSchoolByDistrict_WithCorrectInputData_ShouldReturnSchoolByDistrict()
        {
            this.SeedTestData(this.DbContext);
            int districtId = this.DbContext.Districts.Select(i => i.Id).First();

            List<SchoolServiceModel> expected = this.DbContext.Schools.To<SchoolServiceModel>()
                .Where(d => d.District.Id == districtId)
                .ToList();
            IQueryable<SchoolServiceModel> actual = await this.SchoolsServiceMock
                                                    .GetAllSchoolsByDistrictId(districtId);
            foreach (var data in actual)
            {
                Assert.True(
                        expected.Any(n => n.Name == data.Name),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.Address == data.Address),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.PhoneNumber == data.PhoneNumber),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.DirectorName == data.DirectorName),
                        "SchoolsService GetAllSchools() not works properly!");
                Assert.True(
                        expected.Any(n => n.District.Name == data.District.Name),
                        "SchoolsService GetAllSchools() not works properly!");
            }

        }

        [Fact]
        public async Task GetSchoolByDistrictId_WithCorrectDistrictAndNoSchools_ShouldReturnEmptyList()
        {
            this.DbContext.Districts.Add(new District { Id = 100, Name = "Монте Карло",});
            this.DbContext.SaveChanges();

            int districtId = this.DbContext.Districts.Select(i => i.Id).Last();

            List<SchoolServiceModel> expected = new List<SchoolServiceModel>();
            IQueryable<SchoolServiceModel> actual = await this.SchoolsServiceMock
                                                    .GetAllSchoolsByDistrictId(districtId);

            Assert.True(
                    actual.Count() == 0,
                    "SchoolsService GetSchoolByDistrict() not works properly!");
        }

        [Fact]
        public async Task GetSchoolByDistrict_WithInCorrectDistrictId_ShouldThrowsNullRefException()
        {
            this.SeedTestData(this.DbContext);
            int districtId = 100;

            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.SchoolsServiceMock.GetAllSchoolsByDistrictId(districtId));
        }

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Schools.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<School> GetTestData()
        {
            var district1 = new District { Id = 77, Name = "Капана" };
            var district2 = new District { Id = 88, Name = "Меден Рудник" };

            return new List<School>
            {
                new School
                {
                    Name = "107мо",
                    Address = "ул. Хан Крум",
                    DirectorName = "Мария Мария",
                    PhoneNumber = "02/000001",
                    District = district1,
                },
                new School
                {
                    Name = "108мо",
                    Address = "ул. Хан Кру2",
                    DirectorName = "Мария Мария Mariq",
                    PhoneNumber = "02/0011101",
                    District = district1,
                },
                new School
                {
                    Name = "109то",
                    Address = "ул. Хан Кру22",
                    DirectorName = "Мария Мария Mariq Мария",
                    PhoneNumber = "02/111111",
                    District = district2,
                },
                new School
                {
                    Name = "110то",
                    Address = "ул. Хан Кру222",
                    DirectorName = "Мария Мария Mariq Мария Мария",
                    PhoneNumber = "02/111111",
                    District = district2,
                },
            };
        }
    }
}