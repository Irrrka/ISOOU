namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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

        //Task<bool> Create(ClaimsPrincipal userIdentity, CandidateServiceModel model);
        //IQueryable<CandidateServiceModel> GetCandidates();
        //Task<CandidateServiceModel> GetCandidateById(int id);
        //Task<bool> Edit(int id, ClaimsPrincipal userIdentity, CandidateServiceModel candidateServiceModel);
        //Task<bool> Delete(int id);
        //Task<bool> AddApplications(int id, List<int> schoolApplicationIds);
        //Task<bool> CreateDocumentSubmission(CreateDocumentInputModel input);

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
            this.SeedTestData(this.DbContext);

            int id = 100;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => this.CandidatesServiceMock.GetCandidateById(id));
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
