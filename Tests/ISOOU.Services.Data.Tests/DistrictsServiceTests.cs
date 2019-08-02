namespace ISOOU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Services.Mapping;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Tests.Common;
    using ISOOU.Web.ViewModels;
    using Moq;
    using Xunit;
    using System.Reflection;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using ISOOU.Services.Models;

    public class DistrictsServiceTests : BaseServiceTests
    {
        private IDistrictsService districtsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();

        [Fact]
        public void TestGetAllDistricts_WithCorrectInputData_ShouldReturnAllDistricts()
        {

            var expected = this.GetTestData();
            var actual = this.districtsServiceMock.GetAllDistrictsAsync();

            foreach (var data in actual)
            {
                Assert.True(expected.Any(n => n.Name == data.Name), 
                    "DistrictsService GetAllDistricts() not works properly!");
            }
        }

        [Fact]
        public void TestGetAllDistricts_WithEmptyData_ShouldReturnEmptyList()
        {

            var expected = new List<DistrictViewModel>();
            var actual = this.districtsServiceMock.GetAllDistrictsAsync();

            foreach (var data in actual)
            {
                Assert.True(actual.Count() == 0,
                        "DistrictsService GetAllDistricts() not works properly!");
            }
        }

        [Fact]
        public async Task TestGetDistrictsById_WithExistingId_ShouldReturnDistrictById()
        {
            int id = 1;

            var expected = this.GetTestData().FirstOrDefault(i => i.Id == id);
            var actual = await this.districtsServiceMock.GetDistrictById(id);

            AssertExtensions.EqualWithMessage(
                                                expected.Name,
                                                actual.Name,
                                                "DistrictsService GetDistrictById() not works properly!");
        }

        [Fact]
        public async Task TestGetDistrictsById_WithNonExistingId_ShouldThrowNullReferenceException()
        {
            int id = 5;

            var expected = this.GetTestData().FirstOrDefault(i => i.Id == id);
            //var actual = await this.districtsServiceMock.GetDistrictById<DistrictViewModel>(id);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.districtsServiceMock.GetDistrictById(id));
    }

        private void SeedTestData(ISOOUDbContext context)
        {

            context.Districts.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<District> GetTestData()
        {
            return new List<District>
            {
                new District
                {
                    Name = "Факултета",
                },
                new District
                {
                    Name = "Център",
                },
            };
        }
    }
}
