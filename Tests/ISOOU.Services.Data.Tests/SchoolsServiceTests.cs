namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SchoolsServiceTests : BaseServiceTests
    {
        private ISchoolsService SchoolsServiceMock =>
            this.ServiceProvider.GetRequiredService<ISchoolsService>();

        private IDistrictsService DistrictsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();

        [Fact]
        public void GetAllSchools_WithCorrectInputData_ShouldReturnAllSchools()
        {
            this.SeedTestData(this.DbContext);

            List<SchoolServiceModel> expected = this.DbContext.Schools.To<SchoolServiceModel>().ToList();
            List<SchoolServiceModel> actual = this.SchoolsServiceMock.GetAllSchools().ToList();

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
        public void GetAllSchools_WithEmptyData_ShouldReturnEmptyList()
        {
            List<SchoolServiceModel> expected = new List<SchoolServiceModel>();
            List<SchoolServiceModel> actual = this.SchoolsServiceMock.GetAllSchools().ToList();

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
        public async Task GetSchoolsByDistrict_WithCorrectInputData_ShouldReturnSchoolsByDistrict()
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
        public async Task GetSchoolsByDistrictId_WithCorrectDistrictAndNoSchools_ShouldReturnEmptyList()
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
        public async Task GetSchoolsByDistrict_WithInCorrectDistrictId_ShouldThrowsNullRefException()
        {
            this.SeedTestData(this.DbContext);
            int districtId = 100;

            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.SchoolsServiceMock.GetAllSchoolsByDistrictId(districtId));
        }

        [Fact]
        public async Task GetSchoolIdByName_WithExistName_ShouldReturnSchoolId()
        {
            this.SeedTestData(this.DbContext);

            SchoolServiceModel school = this.DbContext.Schools.To<SchoolServiceModel>().First();
            string name = school.Name;
            int expected = this.DbContext.Schools.First(n => n.Name == name).Id;

            int actual = await this.SchoolsServiceMock.GetSchoolIdByName(name);

            Assert.True(
                        expected == actual,
                        "SchoolsService GetSchoolIdByName() not works properly!");
        }

        [Fact]
        public async Task GetSchoolIdByName_WithNonExistName_ShouldReturnArgumentNullException()
        {
            this.SeedTestData(this.DbContext);
            string schoolName = "120-то";

            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.SchoolsServiceMock.GetSchoolIdByName(schoolName));
        }

        [Fact]
        public async Task GetSchoolForEdit_WithDirectorUser_ShouldReturnDirectorsSchool()
        {
           //TODO
        }

        [Fact]
        public async Task EditSchool_WithExistSchool_ShouldReturnEditedSchool()
        {
            this.SeedTestData(this.DbContext);
            var school = this.DbContext.Schools.To<SchoolServiceModel>().First();
            school.Name = "107мо ОУ";

            var result = await this.SchoolsServiceMock.EditSchool(school.Id, school);

            Assert.True(
                        result == true,
                        "SchoolsService EditSchool() not works properly!");
        }

        [Fact]
        public async Task EditSchool_WithNonExistSchool_ShouldReturnNullRef()
        {
            this.SeedTestData(this.DbContext);
            var school = this.DbContext.Schools.To<SchoolServiceModel>().First();
            school.Name = "107мо ОУ";

            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.SchoolsServiceMock.EditSchool(100, school));
        }

        [Fact]
        public async Task GetAdmittedCandidates_ShouldReturnAdmittedCandidates()
        {
            this.SeedTestData(this.DbContext);
            var candidate1 = this.DbContext.CandidatesApplications.First(x => x.CandidateId == 1);
            candidate1.Candidate.Status = CandidateStatus.Admitted;
            var candidate2 = this.DbContext.CandidatesApplications.First(x => x.CandidateId == 2);
            candidate2.Candidate.Status = CandidateStatus.Admitted;
            var expected = new List<string> { candidate1.Candidate.FullName };
            var notAdmitted = new List<string> { candidate2.Candidate.FullName };

            var actual = await this.SchoolsServiceMock.GetAdmittedCandidates();

            Assert.True(
                        actual.Count == 1,
                        "SchoolsService GetAdmittedCandidates() not works properly!");
        }

        [Fact]
        public async Task GetSchoolsAndCandidates_ShouldReturnListOfCandidates()
        {
            this.SeedTestData(this.DbContext);
            var actual = await this.DbContext.CandidatesApplications.ToListAsync();

            var expecred = this.SchoolsServiceMock.GetSchoolsAndCandidates().ToList();

            Assert.True(
                        actual.Count == expecred.Count,
                        "SchoolsService GetSchoolsAndCandidates() not works properly!");
        }

        [Fact]
        public void GetSchoolsAndCandidates_WithNoCandidatesWithApplications_ReturnsEmptyList()
        {
            var expecred = this.SchoolsServiceMock.GetSchoolsAndCandidates().ToList();

            Assert.True(
                        expecred.Count == 0,
                        "SchoolsService GetSchoolsAndCandidates() not works properly!");
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

            var candidate1 = new Candidate
            {
                FirstName = "Yavor",
                MiddleName = "Ivaylov",
                LastName = "Marinov",
                UCN = "1309070000",
            };
            var candidate2 = new Candidate
            {
                FirstName = "Yasen",
                MiddleName = "Ivaylov",
                LastName = "Ivanov",
                UCN = "1307090000",
            };

            return new List<School>
            {
                new School
                {
                    Name = "107мо",
                    Address = "ул. Хан Крум",
                    DirectorName = "Мария Мария",
                    PhoneNumber = "02/000001",
                    District = district1,
                    Candidates = new List<CandidateApplication>
                    {
                        new CandidateApplication { CandidateId = 1, SchoolId = 1} ,
                        new CandidateApplication { CandidateId = 2, SchoolId = 1} ,
                    },
                },
                new School
                {
                    Name = "108мо",
                    Address = "ул. Хан Кру2",
                    DirectorName = "Мария Мария Mariq",
                    PhoneNumber = "02/0011101",
                    District = district1,
                    Candidates = new List<CandidateApplication>
                    {
                        new CandidateApplication { CandidateId = 1, SchoolId = 2} ,
                        new CandidateApplication { CandidateId = 2, SchoolId = 2} ,
                    },
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