namespace ISOOU.Web.Areas.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
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
        private readonly IDistrictsService districtsService;
        private readonly IAddressesService addressesService;


        public ParentController(
            IParentsService parentsService,
            IDistrictsService districtsService,
            IAddressesService addressesService)
        {
            this.parentsService = parentsService;
            this.districtsService = districtsService;
            this.addressesService = addressesService;
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
            parent.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);

            await this.parentsService.Create(userIdentity, parent);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = (await this.parentsService.GetParentById(id))
                                        .To<EditParentInputModel>();

            if (model == null)
            {
                return this.Redirect("/");
            }

            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

            var allCityNames = new List<string>() { "София", "Друг" };
            this.ViewData["CityNames"] = allCityNames;

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
            address.PermanentCity = (CityName)Enum.Parse(typeof(CityName), input.AddressCurrentCity);
            address.PermanentDistrict = await this.districtsService.GetDistrictByName(input.AddressPermanentDistrictName);

            DistrictServiceModel workDistrict = await this.districtsService.GetDistrictByName(input.WorkDistrictName);

            ClaimsPrincipal userIdentity = this.User;

            //ParentServiceModel parent = parentToEdit.To<ParentServiceModel>();
            parentToEdit.WorkDistrict = workDistrict;
            parentToEdit.Address = address;
            parentToEdit.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);

            await this.parentsService.Edit(id, parentToEdit);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var parentDeleteViewModel = (await this.parentsService.GetParentById(id)).To<DeleteParentViewModel>();
            if (parentDeleteViewModel == null)
            {
                return this.Redirect($"Edit/{id}");
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

            return this.View(parentDeleteViewModel);
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
