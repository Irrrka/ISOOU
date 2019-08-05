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
    using ISOOU.Web.ViewModels.Districts;
    using Moq;
    using Xunit;
    using System.Reflection;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using ISOOU.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class DistrictsServiceTests : BaseServiceTests
    {
        private IDistrictsService DistrictsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();

        [Fact]
        public async Task GetAllDistricts_WithCorrectInputData_ShouldReturnAllDistrictsAsync()
        {
            List<DistrictServiceModel> expected = this.GetTestData().To<DistrictServiceModel>().ToList();
            List<DistrictServiceModel> actual = await this.DistrictsServiceMock.GetAllDistricts().ToListAsync();

            foreach (var data in actual)
            {
                Assert.True(
                        expected.Any(n => n.Name == data.Name),
                        "DistrictsService GetAllDistricts() not works properly!");
            }
        }

        [Fact]
        public void GetAllDistricts_WithEmptyData_ShouldReturnEmptyList()
        {
            List<DistrictServiceModel> expected = new List<DistrictServiceModel>();
            List<DistrictServiceModel> actual = this.DistrictsServiceMock.GetAllDistricts().ToList();

            foreach (var data in actual)
            {
                Assert.True(
                        actual.Count() == 0,
                        "DistrictsService GetAllDistricts() not works properly!");
            }
        }

        [Fact]
        public async Task GetDistrictsById_WithExistingId_ShouldReturnDistrictById()
        {
            this.SeedTestData(this.DbContext);

            DistrictServiceModel expected = this.DbContext.Districts.To<DistrictServiceModel>().First();
            DistrictServiceModel actual = await this.DistrictsServiceMock.GetDistrictById(expected.Id);

            Assert.True(
                        expected.Name == actual.Name,
                        "DistrictsService GetDistrictById() not works properly!");
        }

        [Fact]
        public async Task GetDistrictsById_WithNonExistingId_ShouldThrowNullReferenceException()
        {
            this.SeedTestData(this.DbContext);

            int id = 100;
            await Assert.ThrowsAsync<NullReferenceException>(
                () => this.DistrictsServiceMock.GetDistrictById(id));
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
                    Id = 1,
                    Name = "Факултета",
                },
                new District
                {
                    Id = 2,
                    Name = "Център",
                },
            };
        }
    }
}
