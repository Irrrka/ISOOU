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

    public class CandidateController : UserController
    {
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;
        private readonly IDistrictsService districtsService;
        private readonly ISchoolsService schoolsService;

        public CandidateController(
            IParentsService parentsService,
            ICandidatesService candidatesService,
            IDistrictsService districtsService,
            ISchoolsService schoolsService)
        {
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
            this.districtsService = districtsService;
            this.schoolsService = schoolsService;
        }

        [HttpGet("/Users/Candidate/Create")]
        public IActionResult Create()
        {
            var allusersParents = this.parentsService
                .GetParents()
                .Where(x => x.User.UserName == this.User.Identity.Name);
            this.ViewData["Parents"] = allusersParents
                .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
                .ToList();
            return this.View();
        }

        [HttpPost("/Users/Candidate/Create")]
        public async Task<IActionResult> Create(CreateCandidateInputModel candidateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allusersParents = this.parentsService
              .GetParents()
              .Where(x => x.User.UserName == this.User.Identity.Name);
                this.ViewData["Parents"] = allusersParents
                    .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
                    .ToList();
                return this.View(candidateInputModel);
            }

            var userIdentity = candidateInputModel.UserName;

            CandidateServiceModel candidateToAdd = new CandidateServiceModel();
            //TODO automapper doesnt work why?
            //var father = await this.parentsService.GetParentById(candidateInputModel.FatherId);
            //var mother = await this.parentsService.GetParentById(candidateInputModel.MotherId);
            candidateToAdd.UCN = candidateInputModel.UCN;
            candidateToAdd.FirstName = candidateInputModel.FirstName;
            candidateToAdd.MiddleName = candidateInputModel.MiddleName;
            candidateToAdd.LastName = candidateInputModel.LastName;
            //candidateToAdd.Father = father;
           // candidateToAdd.Mother = mother;
            candidateToAdd.KinderGarten = candidateInputModel.KinderGarten;
            candidateToAdd.SEN = candidateInputModel.SEN;
            candidateToAdd.Desease = candidateInputModel.Desease;
            candidateToAdd.YearOfBirth = candidateInputModel.YearOfBirth;

            await this.candidatesService.Create(userIdentity, candidateToAdd);

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

            var allusersParents = this.parentsService
               .GetParents()
               .Where(x => x.User.UserName == this.User.Identity.Name);
            this.ViewData["Parents"] = allusersParents
                .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
                .ToList();

            return this.View(candidateEditViewModel);
        }

        [HttpPost("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(EditCandidateInputModel candidateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allusersParents = this.parentsService
              .GetParents()
              .Where(x => x.User.UserName == this.User.Identity.Name);
                this.ViewData["Parents"] = allusersParents
                    .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
                    .ToList();
                return this.View(candidateInputModel);
            }

            var candidateToEdit = candidateInputModel.To<CandidateServiceModel>();
            //TODO automapper doesnt work why?
            //var father = await this.parentsService.GetParentById(candidateInputModel.FatherId);
            //var mother = await this.parentsService.GetParentById(candidateInputModel.MotherId);
            candidateToEdit.UCN = candidateInputModel.UCN;
            candidateToEdit.FirstName = candidateInputModel.FirstName;
            candidateToEdit.MiddleName = candidateInputModel.MiddleName;
            candidateToEdit.LastName = candidateInputModel.LastName;
            //candidateToAdd.Father = father;
            // candidateToAdd.Mother = mother;
            candidateToEdit.KinderGarten = candidateInputModel.KinderGarten;
            candidateToEdit.SEN = candidateInputModel.SEN;
            candidateToEdit.Desease = candidateInputModel.Desease;
            candidateToEdit.YearOfBirth = candidateInputModel.YearOfBirth;

            var userIdentity = candidateInputModel.UserName;
            await this.candidatesService.Edit(userIdentity, candidateToEdit);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Criteria")]
        public async Task<IActionResult> Criteria(int id)
        {//NOT WORKING!!!!!!!!
            CandidateServiceModel candidateModel = await this.candidatesService.GetCandidateById(id);

            Dictionary<string, int> result = await this.candidatesService.CalculateScoresByCriteria(id);

            var viewModel = new CalculateScoresByCriteriaOnCandidateViewModel();

            viewModel.ScoresByCrieria = result;

            //?
            viewModel.ParentPermanentCity = candidateModel.Mother.Address.PermanentCity.ToString();
            //?
            viewModel.ParentCurrentCity = candidateModel.Mother.Address.CurrentCity.ToString();
            //?
            viewModel.ParentPermanentDistrictName = candidateModel.Mother.Address.PermanentDistrict.Name;
            //?
            viewModel.ParentCurrentDistrictName = candidateModel.Mother.Address.CurrentDistrict.Name;
            //?
            viewModel.ParentWorkDistrictName = candidateModel.Mother.WorkDistrict.Name;

            viewModel.MotherFullName = candidateModel.Mother.FullName;

            viewModel.FatherFullName = candidateModel.Father.FullName;

            return await Task.Run(() => this.View(viewModel));
        }


        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(int id)
        {
            var allClasses = this.schoolsService
             .GetAllClasses();
            this.ViewData["AllClasses"] = allClasses
                .Select(p => new AddClassApplicationsViewModel { ProfileName = p.Profile.Name, InitialFreeSpots = p.InitialFreeSpots })
                .ToList();

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
                var allClasses = this.schoolsService
                .GetAllClasses();
                this.ViewData["AllClasses"] = allClasses
                    .Select(p => new AddClassApplicationsViewModel { ProfileName = p.Profile.Name, InitialFreeSpots = p.InitialFreeSpots })
                    .ToList()
                    .OrderBy(x => x.ProfileName);

                var allSchools = this.schoolsService
                  .GetAllSchools();
                this.ViewData["AllSchools"] = allSchools
                    .Select(p => new AddSchoolApplicationsViewModel { Id = p.Id, Name = p.Name, DistrictName = p.District.Name })
                    .ToList()
                    .OrderBy(x => x.DistrictName);

                return this.View(input);
            }

            List<CandidateSchoolClassServiceModel> applications = 
                new List<CandidateSchoolClassServiceModel>();
            //TODO Fix this:
            SchoolClassServiceModel schoolClassFirstWish = this.schoolsService.GetSchoolClassBySchoolAndClass(input.FirstWishSchool, input.FirstWishClassProfile);
            SchoolClassServiceModel schoolClassSecondWish = this.schoolsService.GetSchoolClassBySchoolAndClass(input.SecondWishSchool, input.SecondWishClassProfile);
            SchoolClassServiceModel schoolClassThirdWish = this.schoolsService.GetSchoolClassBySchoolAndClass(input.ThirdWishSchool, input.ThirdWishClassProfile);
            List<SchoolClassServiceModel> applicationsToAdd = new List<SchoolClassServiceModel>();
            applicationsToAdd.Add(schoolClassFirstWish);
            applicationsToAdd.Add(schoolClassSecondWish);
            applicationsToAdd.Add(schoolClassThirdWish);

            var candidateId = input.CandidateId;
            var userIdentity = input.UserName;
            await this.candidatesService.AddApplications(candidateId, userIdentity, applicationsToAdd);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Profile")]
        public IActionResult Profile(CandidateProfileViewModel model)
        {

            return this.View();
        }
    }

}
