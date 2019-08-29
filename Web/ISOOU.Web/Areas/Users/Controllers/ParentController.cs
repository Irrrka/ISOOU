namespace ISOOU.Web.Areas.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ISOOU.Common;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.Areas.Users.Controllers;
    using ISOOU.Web.ViewModels.Districts;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    public class ParentController : UserController
    {

        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;
        private readonly IDistrictsService districtsService;
        private readonly IAddressesService addressesService;

        public ParentController(
            IParentsService parentsService,
            IDistrictsService districtsService,
            IAddressesService addressesService,
            ICandidatesService candidatesService)
        {
            this.parentsService = parentsService;
            this.districtsService = districtsService;
            this.addressesService = addressesService;
            this.candidatesService = candidatesService;
        }

        [HttpGet(Name = "Create")]
        public IActionResult Create()
        {
            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

            var allCityNames = new List<string>() { "София", "Друг" };
            this.ViewData["CityNames"] = allCityNames;

            return this.View();
        }

        [HttpPost("/Users/Parent/Create")]
        public async Task<IActionResult> Create(CreateParentInputModel input)
        {

            if (!this.ModelState.IsValid)
            {
                var allParentsRole = new List<string>() { "Майка", "Баща" };
                this.ViewData["ParentsRole"] = allParentsRole;

                var allDistricts = this.districtsService.GetAllDistricts();
                this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

                var allCityNames = new List<string>() { "София", "Друг" };
                this.ViewData["CityNames"] = allCityNames;

                return this.View(input);
            }

            if (input.AddressCurrentDistrictName == null)
            {
                input.AddressCurrentDistrictName = input.AddressPermanentDistrictName;
            }

            if (input.AddressCurrent == null)
            {
                input.AddressCurrent = input.AddressPermanent;
            }

            AddressDetailsServiceModel address = new AddressDetailsServiceModel
            {
                Permanent = input.AddressPermanent,
                Current = input.AddressCurrent,
                CurrentCity = (CityName)Enum.Parse(typeof(CityName), input.AddressCurrentCity),
                CurrentDistrict = await this.districtsService.GetDistrictByName(input.AddressCurrentDistrictName),
                PermanentCity = (CityName)Enum.Parse(typeof(CityName), input.AddressCurrentCity),
                PermanentDistrict = await this.districtsService.GetDistrictByName(input.AddressPermanentDistrictName),
            };

            DistrictServiceModel workDistrict = await this.districtsService.GetDistrictByName(input.WorkDistrictName);

            ClaimsPrincipal userIdentity = this.User;

            ParentServiceModel parent = input.To<ParentServiceModel>();
            parent.WorkDistrict = workDistrict;
            parent.Address = address;

            var role = await this.parentsService.GetParentsRoleByUser(userIdentity);
            if ((role == ParentRole.Майка.ToString() || role == ParentRole.Баща.ToString()) && input.ParentRole == role)
            {
                return this.BadRequest(GlobalConstants.UniqueParentRole);
            }

            parent.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);

            await this.parentsService.Create(userIdentity, parent);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var parent = await this.parentsService.GetParentById(id);

            if (parent == null || parent.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

            var allCityNames = new List<string>() { "София", "Друг" };
            this.ViewData["CityNames"] = allCityNames;

            var model = parent.To<EditParentInputModel>();

            return this.View(model);
        }

        [HttpPost("/Users/Parent/Edit")]
        public async Task<IActionResult> Edit(int id, EditParentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var allParentsRole = new List<string>() { "Майка", "Баща" };
                this.ViewData["ParentsRole"] = allParentsRole;

                var allDistricts = this.districtsService.GetAllDistricts();
                this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

                var allCityNames = new List<string>() { "София", "Друг" };
                this.ViewData["CityNames"] = allCityNames;

                return this.View(input);
            }

            ParentServiceModel parentToEdit = await this.parentsService.GetParentById(id);
            var addressId = parentToEdit.AddressId;
            AddressDetailsServiceModel address = await this.addressesService.GetAddressDetailsById(addressId);

            address.Permanent = input.AddressPermanent;
            address.Current = input.AddressCurrent;
            address.CurrentCity = (CityName)Enum.Parse(typeof(CityName), input.AddressCurrentCity);
            address.CurrentDistrict = await this.districtsService.GetDistrictByName(input.AddressCurrentDistrictName);
            address.CurrentDistrictId = address.CurrentDistrict.Id;
            address.PermanentCity = (CityName)Enum.Parse(typeof(CityName), input.AddressCurrentCity);
            address.PermanentDistrict = await this.districtsService.GetDistrictByName(input.AddressPermanentDistrictName);
            address.PermanentDistrictId = address.PermanentDistrict.Id;

            DistrictServiceModel workDistrict = await this.districtsService.GetDistrictByName(input.WorkDistrictName);

            ClaimsPrincipal userIdentity = this.User;

            parentToEdit.FirstName = input.FirstName;
            parentToEdit.MiddleName = input.MiddleName;
            parentToEdit.LastName = input.LastName;
            parentToEdit.PhoneNumber = input.PhoneNumber;
            parentToEdit.WorkName = input.WorkName;

            parentToEdit.WorkDistrictId = workDistrict.Id;
            parentToEdit.Address = address;
            parentToEdit.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);

            await this.parentsService.Edit(id, parentToEdit);

            var candidatesOfParents = this.candidatesService.GetCandidatesOfParent(id).ToList();

            foreach (var candidate in candidatesOfParents)
            {
                await this.candidatesService.EditDataFromParents(candidate.Id);
            }

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await this.parentsService.GetParentById(id);

            if (parent == null || parent.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;

            var model = parent.To<DeleteParentViewModel>();

            return this.View(model);
        }

        [HttpPost]
        [Route("/Users/Parent/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await this.parentsService.Delete(id);

            return this.Redirect("/");
        }
    }
}
