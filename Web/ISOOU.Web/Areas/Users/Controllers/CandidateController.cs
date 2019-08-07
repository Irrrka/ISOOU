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

    public class CandidateController : UserController
    {
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;
        private readonly IDistrictsService districtsService;

        public CandidateController(
            IParentsService parentsService,
            ICandidatesService candidatesService,
            IDistrictsService districtsService)
        {
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
            this.districtsService = districtsService;
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
        public IActionResult Criteria(int id)
        {
            //var candidateEditViewModel = (await this.candidatesService.GetCandidateById(id))
            //                        .To<EditCandidateInputModel>();

            //if (candidateEditViewModel == null)
            //{
            //    return this.Redirect("/");
            //}

            //var allusersParents = this.parentsService
            //   .GetParents()
            //   .Where(x => x.User.UserName == this.User.Identity.Name);
            //this.ViewData["Parents"] = allusersParents
            //    .Select(p => new CreateCandidateParentViewModel { Id = p.Id, FullName = p.FullName })
            //    .ToList();

            return this.View();
        }

        [HttpGet(Name = "Applications")]
        public IActionResult Applications(int id)
        {
            //TODO Applications
            return this.View();
        }

        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplicationsAsync(int id, CalculateScoresByCriteriaOnCandidateViewModel model)
        {
            var allDistricts = this.districtsService.GetAllDistricts();

            this.ViewData["Districts"] = allDistricts.Select(d => new AddApplicationsDistrictViewModel
                                                {
                                                    Name = d.Name,
                                                })
                                                .ToList();

            var scoresByCriteria = await this.candidatesService.CalculateScoresByCriteria(id);

            return this.View(scoresByCriteria);
        }

        [HttpPost(Name = "AddApplications")]
        public IActionResult AddApplications(ScoresByApplicationsViewModel applicationsListViewModel)
        {
            //TODO Applications
            return this.View();
        }
    }

}
