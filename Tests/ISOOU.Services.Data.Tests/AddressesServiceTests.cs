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
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AddressesServiceTests : BaseServiceTests
    {
        private IAddressesService AddressesServiceMock =>
            this.ServiceProvider.GetRequiredService<IAddressesService>();

        [Fact]
        public async Task GetAddressById_WithCorrectInputData_ShouldReturnAddressDetails()
        {
            this.SeedTestData(this.DbContext);
            AddressDetailsServiceModel expected = this.DbContext.Addresses.First().To<AddressDetailsServiceModel>();
            AddressDetailsServiceModel actual = await this.AddressesServiceMock.GetAddressDetailsById(1);

            Assert.True(
                        expected.PermanentDistrictId == actual.PermanentDistrictId,
                        "AddressesService GetAddressById() not works properly!");
            Assert.True(
                        expected.CurrentDistrictId == actual.CurrentDistrictId,
                        "AddressesService GetAddressById() not works properly!");
            Assert.True(
                     expected.Permanent == actual.Permanent,
                     "AddressesService GetAddressById() not works properly!");
            Assert.True(
                     expected.Current == actual.Current,
                     "AddressesService GetAddressById() not works properly!");
        }

        [Fact]
        public async Task GetAddressById_WithNonExistId_ShouldReturnNullRef()
        {
            this.SeedTestData(this.DbContext);
            int id = 100;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => this.AddressesServiceMock.GetAddressDetailsById(id));
        }

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Addresses.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<AddressDetails> GetTestData()
        {
            var district = new District
            {
                Id = 1,
                Name = "Факултета",
            };

            return new List<AddressDetails>
            {
                new AddressDetails
                {
                    Id = 1,
                    CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                    CurrentDistrictId = 1,
                    Current = "Ruda 1",
                    PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                    PermanentDistrictId = 1,
                    Permanent = "Ruda 1",
                },
                new AddressDetails
                {
                    Id = 2,
                    CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                    CurrentDistrictId = 1,
                    Current = "Jelyazo 11",
                    PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                    PermanentDistrictId = 1,
                    Permanent = "Jelyazo 11",
                },
            };
        }
    }
}
