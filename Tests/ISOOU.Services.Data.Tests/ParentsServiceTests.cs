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
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ParentsServiceTests : BaseServiceTests
    {
        private IParentsService ParentssServiceMock =>
            this.ServiceProvider.GetRequiredService<IParentsService>();

        //TODO How to test with ClaimsPrincipal

        //Task<bool> Create(ClaimsPrincipal userIdentity, ParentServiceModel parentServiceModel);
        //IQueryable<ParentServiceModel> GetParents(ClaimsPrincipal userIdentity);
        //IQueryable<ParentServiceModel> GetParentsWithOtherAndNull(ClaimsPrincipal userIdentity);
        //Task<string> GetParentFullNameByRole(ClaimsPrincipal userIdentity, ParentRole role);
        //Task<int> GetParentIdByFullName(ClaimsPrincipal userIdentity, string fullName);
        //Task<string> GetParentsRoleByUser(ClaimsPrincipal userIdentity);

        [Fact]
        public async Task GetParentById_WithCorrectInputData_ShouldReturnParent()
        {
            this.SeedTestData(this.DbContext);

            ParentServiceModel expected = this.DbContext.Parents.First().To<ParentServiceModel>();

            ParentServiceModel actual = await this.ParentssServiceMock.GetParentById(expected.Id);

            Assert.True(
                        expected.FirstName == actual.FirstName,
                        "ParentsService GetParentById() not works properly!");
            Assert.True(
                       expected.MiddleName == actual.MiddleName,
                       "ParentsService GetParentById() not works properly!");
            Assert.True(
                       expected.LastName == actual.LastName,
                       "ParentsService GetParentById() not works properly!");
            Assert.True(
                       expected.UCN == actual.UCN,
                       "ParentsService GetParentById() not works properly!");
        }

        [Fact]
        public async Task GetParentById_WithInCorrectId_ShouldReturnNull()
        {
            this.SeedTestData(this.DbContext);

            int id = 100;
            var parent = await this.ParentssServiceMock.GetParentById(id);
            Assert.True(
                       parent == null,
                       "ParentsService GetParentById() not works properly!");
        }

        [Fact]
        public async Task Edit_ShouldEditParent()
        {
            this.SeedTestData(this.DbContext);

            ParentServiceModel parentToEdit = this.DbContext.Parents.First().To<ParentServiceModel>();

            parentToEdit.PhoneNumber = "00000000";

            var result = await this.ParentssServiceMock.Edit(parentToEdit.Id, parentToEdit);

            ParentServiceModel editedParent = this.DbContext.Parents.First().To<ParentServiceModel>();

            Assert.True(
                        result = true,
                        "ParentsService Edit() not works properly!");
            Assert.True(
                        editedParent.PhoneNumber == "00000000",
                        "ParentsService Edit() not works properly!");
        }

        [Fact]
        public async Task EditParent_WithInCorrectId_ShouldReturnNullRef()
        {
            this.SeedTestData(this.DbContext);
            ParentServiceModel parentToEdit = this.DbContext.Parents.First().To<ParentServiceModel>();

            int id = 100;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => this.ParentssServiceMock.Edit(id, parentToEdit));
        }

        [Fact]
        public async Task DeleteParent_ShouldDeleteParent()
        {
            this.SeedTestData(this.DbContext);

            ParentServiceModel parentToDelete = this.DbContext.Parents.First().To<ParentServiceModel>();

            var result = await this.ParentssServiceMock.Delete(parentToDelete.Id);

            Assert.True(
                        result = true,
                        "ParentsService Edit() not works properly!");
        }

        [Fact]
        public async Task DeleteParent_WithInCorrectId_ShouldReturnNullRef()
        {
            this.SeedTestData(this.DbContext);

            int id = 100;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => this.ParentssServiceMock.Delete(id));
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
                new Parent
                {
                    Address = new AddressDetails
                    {
                        Id = 1,
                        CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                        CurrentDistrict = new District
                        {
                            Id = 1,
                            Name = "Факултета",
                        },
                        Current = "Ruda 1",
                        PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                        PermanentDistrict = new District
                        {
                            Id = 2,
                            Name = "Стулипиново",
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
                        Name = "Филиповци",
                    },
                },
                new Parent
                {
                    Address = new AddressDetails
                    {
                        Id = 2,
                        CurrentCity = ISOOU.Data.Models.Enums.CityName.София,
                        CurrentDistrict = new District
                        {
                            Id = 4,
                            Name = "Монмартр",
                        },
                        Current = "Ruda 1",
                        PermanentCity = ISOOU.Data.Models.Enums.CityName.София,
                        PermanentDistrict = new District
                        {
                            Id = 5,
                            Name = "Живовци",
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
            };
        }
    }
}
