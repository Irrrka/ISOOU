namespace ISOOU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CriteriasServiceTests : BaseServiceTests
    {
        private ICriteriasService CriteriasServiceMock =>
            this.ServiceProvider.GetRequiredService<ICriteriasService>();

        [Fact]
        public async Task GetIdByCriteriaName_WithCorrectInputData_ShouldReturnCriteriaIdAsync()
        {
            this.SeedTestData(this.DbContext);

            var expected = this.DbContext.Criterias.First();
            var actual = await this.CriteriasServiceMock.GetIdByCriteriaName(expected.Name);

            Assert.True(
                        expected.Id == actual,
                        "CriteriasService  GetIdByCriteriaName() not works properly!");
        }

        [Fact]
        public async Task GetIdByCriteriaName_WithNonExistingInputData_ShouldReturnNullRef()
        {
            this.SeedTestData(this.DbContext);

            await Assert.ThrowsAsync<NullReferenceException>(
               () => this.CriteriasServiceMock.GetIdByCriteriaName("Test test"));
        }

        [Fact]
        public async Task GetAllCriterias_WithCorrectInputData_ShouldReturnAllCriterias()
        {
            this.SeedTestData(this.DbContext);

            var expected = await this.DbContext.Criterias.ToListAsync();
            var actual = await this.CriteriasServiceMock.GetAllCriterias();

            foreach (var criteria in actual)
            {
                Assert.True(
                       expected.Any(n => n.Name == criteria.Name),
                       "CriteriasService GetAllCriterias() not works properly!");
                Assert.True(
                      expected.Any(n => n.DisplayName == criteria.DisplayName),
                      "CriteriasService GetAllCriterias() not works properly!");
                Assert.True(
                      expected.Any(n => n.Scores == criteria.Scores),
                      "CriteriasService GetAllCriterias() not works properly!");
            }
        }

        [Fact]
        public async Task GetAllCriterias_WithCorrectNonExistingInputData_ShouldReturnEmptyList()
        {
            var actual = await this.CriteriasServiceMock.GetAllCriterias();

            Assert.True(
                       actual.Count() == 0,
                       "CriteriasService GetAllCriterias() not works properly!");
        }

        //TODO GetCriteriasAndScoresByCandidateId
        //TODO DeleteCriteriasByCandidateId

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Criterias.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<Criteria> GetTestData()
        {
            var criteria1 = new Criteria { DisplayName = "Дядо му е Коледа", Name = "DqdoMuEKoleda", Scores = 100 };
            var criteria2 = new Criteria { DisplayName = "Баба му е Яга", Name = "BabaMuEQga", Scores = 50 };

            return new List<Criteria> { criteria1, criteria2 };
        }
    }
}