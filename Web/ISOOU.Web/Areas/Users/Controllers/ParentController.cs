namespace ISOOU.Web.Areas.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Web.Areas.Users.Controllers;
    using ISOOU.Web.Areas.Users.Models;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ISOOU.Web.ViewModels.Districts;
    using System.Collections.Generic;
    using System;
    using ISOOU.Common;

    public class ParentController : UserController
    {
        private readonly IParentsService parentsService;
        private readonly IDistrictsService districtsService;

        public ParentController(
            IParentsService parentsService,
            IDistrictsService districtsService)
        {
            this.parentsService = parentsService;
            this.districtsService = districtsService;
        }

        [HttpGet(Name = "Create")]
        public IActionResult Create()
        {
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

            return this.View();
        }

        [HttpPost("/Users/Parent/Create")]
        public async Task<IActionResult> Create(CreateParentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts
                .Select(d => new CreateParentDistrictViewModel {Name = d.Name}).ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }
            this.ViewData["CityNames"] = allCityNames;

            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

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

            ParentServiceModel parent = input.To<ParentServiceModel>();
            parent.WorkDistrict = workDistrict;
            parent.Address = address;
            parent.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);
            var userIdentity = input.UserName;
            await this.parentsService.Create(userIdentity, parent);

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

        [HttpGet(Name = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var parentEditViewModel = (await this.parentsService.GetParentById(id)).To<EditParentInputModel>();
            if (parentEditViewModel == null)
            {
                return this.Redirect("/");
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


            return this.View(parentEditViewModel);
        }

        [HttpPost("/Users/Parent/Edit")]
        public async Task<IActionResult> Edit(EditParentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var allParentsRole = new List<string>() { "Майка", "Баща" };
            this.ViewData["ParentsRole"] = allParentsRole;

            var allDistricts = this.districtsService.GetAllDistricts();
            this.ViewData["Districts"] = allDistricts
                .Select(d => new CreateParentDistrictViewModel { Name = d.Name }).ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }
            this.ViewData["CityNames"] = allCityNames;

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

            ParentServiceModel parent = input.To<ParentServiceModel>();
            
            parent.WorkDistrict = workDistrict;
            parent.Address = address;
            parent.Role = (ParentRole)Enum.Parse(typeof(ParentRole), input.ParentRole);
            var userIdentity = input.UserName;
            await this.parentsService.Edit(userIdentity, parent);

            return this.Redirect("/");
        }
    }
}
