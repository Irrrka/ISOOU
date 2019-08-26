namespace ISOOU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ParentsServiceTests : BaseServiceTests
    {
        private IParentsService ParentssServiceMock =>
            this.ServiceProvider.GetRequiredService<IParentsService>();

        [Fact]
        public async Task GetAllDistricts_WithCorrectInputData_ShouldReturnAllDistrictsAsync()
        {
           
        }

       

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Parents.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<Parent> GetTestData()
        {
            return new List<Parent>
            {
                //new Parent
                //{
                //    Id = 1,
                //    FirstName = "Az",
                //    MiddleName = "Az",
                //    LastName = "Az",
                //    PhoneNumber = "02",
                //    Address = new AddressDetails
                //    {
                //        CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                //        CurrentDistrict = 
                //    }
                //},
               
            };
        }
    }
}
