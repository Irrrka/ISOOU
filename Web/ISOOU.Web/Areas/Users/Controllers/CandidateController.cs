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
    using ISOOU.Web.ViewModels.Districts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Search;
    using ISOOU.Common;
    using System.Security.Claims;

    public class CandidateController : UserController
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;
        private readonly IDistrictsService districtsService;
        private readonly ISchoolsService schoolsService;

        public CandidateController(
            UserManager<SystemUser> userManager,
            IParentsService parentsService,
            ICandidatesService candidatesService,
            IDistrictsService districtsService,
            ISchoolsService schoolsService)
        {
            this.userManager = userManager;
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
            this.districtsService = districtsService;
            this.schoolsService = schoolsService;
        }

        [HttpGet("/Users/Candidate/Create")]
        public async Task<IActionResult> Create()
        {
            var motherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Майка);

            var motherList = new List<string>
            {
                $"{motherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Неизвестен.ToString(),
            };

            var fatherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Баща);

            var fatherList = new List<string>
            {
                $"{fatherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Неизвестен.ToString(),
            };

            this.ViewData["Mother"] = motherList;
            this.ViewData["Father"] = fatherList;

            return this.View();
        }

        [HttpPost("/Users/Candidate/Create")]
        public async Task<IActionResult> Create(CreateCandidateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var motherFullName = await this.parentsService
                    .GetParentFullNameByRole(this.User, ParentRole.Майка);

                var motherList = new List<string>
                 {
                    $"{motherFullName}",
                    ParentRole.Друг.ToString(),
                    ParentRole.Неизвестен.ToString(),
                 };

                var fatherFullName = await this.parentsService
                 .GetParentFullNameByRole(this.User, ParentRole.Баща);

                var fatherList = new List<string>
                {
                    $"{fatherFullName}",
                    ParentRole.Друг.ToString(),
                    ParentRole.Неизвестен.ToString(),
                };

                this.ViewData["Mother"] = motherList;
                this.ViewData["Father"] = fatherList;
                return this.View(input);
            }

            ClaimsPrincipal userIdentity = this.User;

            var parents = this.parentsService.GetParents(userIdentity);
            ParentServiceModel motherServiceModel = await parents.Where(p => p.FullName.Equals(input.MotherFullName)).FirstOrDefaultAsync();
            ParentServiceModel fatherServiceModel = await parents.Where(p => p.FullName.Equals(input.FatherFullName)).FirstOrDefaultAsync();

            CandidateServiceModel model = input.To<CandidateServiceModel>();
            model.MotherId = motherServiceModel.Id;
            model.FatherId = fatherServiceModel.Id;

            await this.candidatesService.Create(userIdentity, model);

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"/Users/Candidate/Edit/{id}");
            }

            //TODO
            await this.candidatesService.Delete(id);

            return this.Redirect("/");
        }

        [HttpGet("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var candidateEditViewModel = (await this.candidatesService.GetCandidateById(id))
                                    .To<EditCandidateInputModel>();

            if (candidateEditViewModel == null)
            {
                return this.Redirect("/");
            }

            return this.View(candidateEditViewModel);
        }

        [HttpPost("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(int id, EditCandidateInputModel candidateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
              // var allusersParents = this.parentsService
              //.GetParents()
              //.Where(x => x.User.UserName == this.User.Identity.Name);
              //  this.ViewData["Parents"] = allusersParents
              //      .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
              //      .ToList();
                return this.View(candidateInputModel);
            }

            var candidateToEdit = candidateInputModel.To<CandidateServiceModel>();
            
            // var parents = await this.parentsService.GetParents(userIdentity).ToListAsync();mmm
            //foreach (var parent in parents)
            //{
            //    candidateToEdit.CandidateParents.Add(
            //        new CandidateParentServiceModel { ParentId = parent.Id });
            //}
            ClaimsPrincipal userIdentity = this.User;

            await this.candidatesService.Edit(id, userIdentity, candidateToEdit);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Criteria")]
        public async Task<IActionResult> Criteria(int id)
        {//NOT WORKING!!!!!!!!
            CandidateServiceModel candidateModel = await this.candidatesService.GetCandidateById(id);

            //Dictionary<string, int> result = await this.candidatesService.CalculateScoresByCriteria(id);

            var viewModel = new CalculateScoresByCriteriaOnCandidateViewModel();

            //viewModel.ScoresByCrieria = result;

            //?
            //viewModel.ParentPermanentCity = candidateModel.Mother.Address.PermanentCity.ToString();
            ////?
            //viewModel.ParentCurrentCity = candidateModel.Mother.Address.CurrentCity.ToString();
            ////?
            //viewModel.ParentPermanentDistrictName = candidateModel.Mother.Address.PermanentDistrict.Name;
            ////?
            //viewModel.ParentCurrentDistrictName = candidateModel.Mother.Address.CurrentDistrict.Name;
            ////?
            //viewModel.ParentWorkDistrictName = candidateModel.Mother.WorkDistrict.Name;

            //viewModel.MotherFullName = candidateModel.Mother.FullName;

            //viewModel.FatherFullName = candidateModel.Father.FullName;

            return await Task.Run(() => this.View(viewModel));
        }


        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(int id)
        {
            var allSchools = this.schoolsService
              .GetAllSchools();
            this.ViewData["AllSchools"] = allSchools
                .Select(p => new AddSchoolApplicationsViewModel { Id = p.Id, Name = p.Name, DistrictName = p.District.Name })
                .ToList();

            CandidateServiceModel candidateServiceModel = await this.candidatesService.GetCandidateById(id);
            AddApplicationsInputModel model = new AddApplicationsInputModel();
            model.CandidateId = candidateServiceModel.Id;

            return this.View(model);
        }

        [HttpPost(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(AddApplicationsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var allSchools = this.schoolsService
                  .GetAllSchools();
                this.ViewData["AllSchools"] = allSchools
                    .Select(p => new AddSchoolApplicationsViewModel { Id = p.Id, Name = p.Name, DistrictName = p.District.Name })
                    .ToList()
                    .OrderBy(x => x.DistrictName);

                return this.View(input);
            }

            var applicationsToAdd = new List<SchoolCandidateServiceModel>();
            //TODO Fix this:
            //var schoolFirstWish = this.schoolsService.GetSchoolDetailsByName(input.FirstWishSchool);
            //var schoolSecondWish = this.schoolsService.GetSchoolDetailsByName(input.SecondWishSchool);
            //var schoolThirdWish = this.schoolsService.GetSchoolDetailsByName(input.ThirdWishSchool);
            //applicationsToAdd.Add(schoolFirstWish);
            //applicationsToAdd.Add(schoolSecondWish);
            //applicationsToAdd.Add(schoolThirdWish);

            //var candidateId = input.CandidateId;
            //var userIdentity = input.UserName;
            //await this.candidatesService.AddApplications(candidateId, userIdentity, applicationsToAdd);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Profile")]
        public IActionResult Profile(CandidateProfileViewModel model)
        {

            return this.View();
        }
    }

}
