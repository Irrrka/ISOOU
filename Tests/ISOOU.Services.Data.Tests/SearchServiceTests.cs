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

    public class SearchServiceTests : BaseServiceTests
    {
        private ISearchService SearchServiceMock =>
            this.ServiceProvider.GetRequiredService<ISearchService>();
        private IDistrictsService DistrictsServiceMock =>
            this.ServiceProvider.GetRequiredService<IDistrictsService>();
        private ISchoolsService SchoolsServiceMock =>
            this.ServiceProvider.GetRequiredService<ISchoolsService>();


        [Theory]
        [InlineData(2012, 2)]
        [InlineData(2013, 1)]
        public async Task GetSearchResult_WithCorrectInputData_ShouldReturnCorrectData(int year, int distriId)
        {
            this.SeedTestData(this.DbContext);
            var schools = this.DbContext.Schools.To<SchoolServiceModel>().ToList();
            var classProfiles = this.DbContext.ClassProfiles.To<ClassProfileServiceModel>().ToList();
            //TODO
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
                    SchoolClasses = new List<SchoolClass>()
                    {
                       new SchoolClass()
                       {
                           Class = new Class()
                           {
                               Profile = new ClassProfile { Name = "Карате"}, InitialFreeSpots = 10,
                           },
                       },
                       new SchoolClass()
                       {
                           Class = new Class()
                           {
                               Profile = new ClassProfile { Name = "Джудо"}, InitialFreeSpots = 10,
                           },
                       },
                    },
                },
                 new School
                {
                    Name = "108мо",
                    Address = "ул. Хан Кру2",
                    DirectorName = "Мария Мария Mariq",
                    PhoneNumber = "02/0011101",
                    District = district1,
                    SchoolClasses = new List<SchoolClass>()
                    {
                       new SchoolClass()
                       {
                           Class = new Class()
                           {
                               Profile = new ClassProfile { Name = "Карате"}, InitialFreeSpots = 10,
                           },
                       },
                    },
                },
                   new School
                {
                    Name = "109то",
                    Address = "ул. Хан Кру22",
                    DirectorName = "Мария Мария Mariq Мария",
                    PhoneNumber = "02/111111",
                    District = district2,
                    SchoolClasses = new List<SchoolClass>()
                    {
                       new SchoolClass()
                       {
                           Class = new Class()
                           {
                               Profile = new ClassProfile { Name = "Джудо"}, InitialFreeSpots = 10,
                           },
                       },
                    },
                },
                      new School
                {
                    Name = "110то",
                    Address = "ул. Хан Кру222",
                    DirectorName = "Мария Мария Mariq Мария Мария",
                    PhoneNumber = "02/111111",
                    District = district2,
                    SchoolClasses = new List<SchoolClass>()
                    {
                       new SchoolClass()
                       {
                           Class = new Class()
                           {
                               Profile = new ClassProfile { Name = "Карате"}, InitialFreeSpots = 10,
                           },
                       },
                    },
                },
            };
        }
    }
}