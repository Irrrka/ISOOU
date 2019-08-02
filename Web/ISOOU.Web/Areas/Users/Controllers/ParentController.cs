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
        public async Task<IActionResult> Create()
        {
            var motherAndFather = await this.parentsService.GetParents();
            var parentsCount = motherAndFather.Count();

            string currentRole = string.Empty;

            //PASS CurrROle
            if (parentsCount == 0)
            {
                currentRole = ParentRole.Майка.ToString();
            }

            if (parentsCount == 1)
            {
                currentRole = ParentRole.Баща.ToString();
            }

            var allDistricts = this.districtsService.GetAllDistrictsAsync();

            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel
                                                 {
                                                    Name = d.Name,
                                                 })
                                                .ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;

            return this.View();
        }

        [HttpPost("/Users/Parent/Create")]
        public async Task<IActionResult> Create(CreateParentInputModel createParentInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createParentInputModel);
            }

            var allDistricts = this.districtsService.GetAllDistrictsAsync();

            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel
            {
                Name = d.Name,
            })
                                                .ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;

            var parentServiceModel = AutoMapper.Mapper.Map<ParentServiceModel>(createParentInputModel);
            await this.parentsService.Create(parentServiceModel);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"/Users/Parents/Edit/{id}");
            }

            var parentTodelete = (await this.parentsService.Delete(id)).To<DeleteParentViewModel>();

            if (parentTodelete == null)
            {
                return this.Redirect("/");
            }

            var allDistricts = this.districtsService.GetAllDistrictsAsync();

            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel
            {
                Name = d.Name,
            })
                                                .ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;

            return this.View(parentTodelete);
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

            var allDistricts = this.districtsService.GetAllDistrictsAsync();

            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel
            {
                Name = d.Name,
            })
                                                .ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;

            return this.View(parentEditViewModel);
        }

        [HttpPost("/Users/Parent/Edit")]
        public async Task<IActionResult> Edit(EditParentInputModel parentInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(parentInputModel);
            }
            //TODO
            var parentServiceModel = AutoMapper.Mapper.Map<ParentServiceModel>(parentInputModel);
            await this.parentsService.Edit(parentServiceModel);

            var allDistricts = this.districtsService.GetAllDistrictsAsync();

            this.ViewData["Districts"] = allDistricts.Select(d => new CreateParentDistrictViewModel
            {
                Name = d.Name,
            })
                                                .ToList();

            var allCityNames = new List<string>();
            foreach (var name in Enum.GetNames(typeof(CityName)))
            {
                allCityNames.Add(name);
            }

            this.ViewData["CityNames"] = allCityNames;
            return this.Redirect("/");
        }
    }
}
