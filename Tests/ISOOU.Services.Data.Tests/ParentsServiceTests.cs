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
