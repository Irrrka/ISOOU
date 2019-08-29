namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Models;
    using ISOOU.Services.Mapping;

    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CandidatesServiceTests : BaseServiceTests
    {
        private ICandidatesService CandidatesServiceMock =>
            this.ServiceProvider.GetRequiredService<ICandidatesService>();

        //TODO How to test with Cloudinary

        //Task<bool> CreateDocumentSubmission(CreateDocumentInputModel input);

        //Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model);
        //Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel);

        //Task<bool> AddApplications(int id, List<int> schoolApplicationIds);

        [Fact]
        public async Task GetCandidateById_WithCorrectInputData_ShouldReturnCandidate()
        {
            this.SeedTestData(this.DbContext);

            CandidateServiceModel expected = this.DbContext.Candidates.First().To<CandidateServiceModel>();

            CandidateServiceModel actual = await this.CandidatesServiceMock.GetCandidateById(expected.Id);

            Assert.True(
                        expected.FirstName == actual.FirstName,
                        "CandidatesService GetCandidateById() not works properly!");
            Assert.True(
                        expected.MiddleName == actual.MiddleName,
                        "CandidatesService GetCandidateById() not works properly!");
            Assert.True(
                        expected.LastName == actual.LastName,
                        "CandidatesService GetCandidateById() not works properly!");
            Assert.True(
                        expected.UCN == actual.UCN,
                        "CandidatesService GetCandidateById() not works properly!");
            Assert.True(
                        expected.Mother.FullName == actual.Mother.FullName,
                        "CandidatesService GetCandidateById() not works properly!");
            Assert.True(
                        expected.Father.FullName == actual.Father.FullName,
                        "CandidatesService GetCandidateById() not works properly!");
        }

        [Fact]
        public async Task GetCandidateById_WithInCorrectId_ShouldReturnNullRef()
        {
            int id = 100;
            var candidate = await this.CandidatesServiceMock.GetCandidateById(id);
            Assert.True(
                        candidate == null,
                        "CandidatesService GetCandidateById() not works properly!");
        }

        [Fact]
        public void GetCandidates_WithSeededData_ShouldReturnCandidateList()
        {
            this.SeedTestData(this.DbContext);

            var expected = this.DbContext.Candidates.To<CandidateServiceModel>().ToList();

            var actual = this.CandidatesServiceMock.GetCandidates().ToList();

            Assert.True(
                        expected.Count == actual.Count,
                        "CandidatesService GetCandidates() not works properly!");
        }

        [Fact]
        public void GetCandidates_WithNoCandidates_ShouldReturnEmptyList()
        {
            var actual = this.CandidatesServiceMock.GetCandidates().ToList();

            Assert.True(
                        actual.Count == 0,
                        "CandidatesService GetCandidates() not works properly!");
        }

        [Fact]
        public void Delete_WithSeededData_ShouldDeleteCandidate()
        {
            this.SeedTestData(this.DbContext);

            var candidateTodelete = this.DbContext.Candidates.To<CandidateServiceModel>().First().Id;

            var actual = this.CandidatesServiceMock.Delete(candidateTodelete);
            var deletedCandidate = this.DbContext.Candidates.To<CandidateServiceModel>()
                                            .FirstOrDefault(i=>i.Id == candidateTodelete);

            Assert.True(
                        deletedCandidate == null,
                        "CandidatesService Delete() not works properly!");
        }

        [Fact]
        public async Task Delete_WithIncorrectData_ShouldThrowNullRef()
        {
            int id = 100;
            var candidate = await this.CandidatesServiceMock.Delete(id);
            await Assert.ThrowsAsync<ArgumentNullException>(
                        () => this.CandidatesServiceMock.Delete(id));
        }

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Candidates.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<Candidate> GetTestData()
        {
            return new List<Candidate>
            {
                new Candidate
                {
                FirstName = "Yavorcho",
                MiddleName = "Byavorcho",
                LastName = "Marinko",
                UCN = "1314151617",
                Father = new Parent
                {
                    Address = new AddressDetails
                    {
                        Id = 1,
                        CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                        CurrentDistrict = new District
                        {
                            Id = 1,
                            Name = "test",
                        },
                        Current = "test 1",
                        PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                        PermanentDistrict = new District
                        {
                            Id = 2,
                            Name = "test2",
                        },
                    },
                    FirstName = "Ivan",
                    MiddleName = "Ivanov",
                    LastName = "Ivanov",
                    PhoneNumber = "08888888",
                    Role = ISOOU.Data.Models.Enums.ParentRole.Баща,
                    UCN = "8002220000",
                    WorkDistrict = new District
                    {
                        Id = 3,
                        Name = "test3",
                    },
                },
                Mother = new Parent
                {
                    Address = new AddressDetails
                    {
                        Id = 2,
                        CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                        CurrentDistrict = new District
                        {
                            Id = 4,
                            Name = "test4",
                        },
                        Current = "test 1",
                        PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                        PermanentDistrict = new District
                        {
                            Id = 5,
                            Name = "test5",
                        },
                    },
                    FirstName = "Ivana",
                    MiddleName = "Ivanova",
                    LastName = "Ivanova",
                    PhoneNumber = "07777777",
                    Role = ISOOU.Data.Models.Enums.ParentRole.Майка,
                    UCN = "0987654321",
                    WorkDistrict = new District
                    {
                        Id = 6,
                        Name = "Удома",
                    },
                },
                },
            };
        }
    }
}
