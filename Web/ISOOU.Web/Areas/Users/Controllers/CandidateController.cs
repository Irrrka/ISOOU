﻿namespace ISOOU.Web.Areas.Users
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

    public class CandidateController : UserController
    {
        private readonly IParentsService parentsService;
        private readonly ICandidatesService candidatesService;

        public CandidateController(
            IParentsService parentsService,
            ICandidatesService candidatesService)
        {
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
        }

        [HttpGet("/Users/Candidate/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("/Users/Candidate/Create")]
        public async Task<IActionResult> Create(CreateCandidateInputModel candidateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(candidateInputModel);
            }

            var candidateServiceModel = AutoMapper.Mapper.Map<CandidateServiceModel>(candidateInputModel);
            await this.candidatesService.Create(candidateServiceModel);

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
        public IActionResult Edit(int id)
        {
            var candidate = this.candidatesService.GetCandidateById(id);
            var candidateProfileViewModel = candidate.To<CandidateProfileViewModel>();
            return this.View(candidateProfileViewModel);
        }

        [HttpPost("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(EditCandidateInputModel candidateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(candidateInputModel);
            }

            var candidateServiceModel = AutoMapper.Mapper.Map<CandidateServiceModel>(candidateInputModel);
            await this.candidatesService.Edit(candidateServiceModel);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Criteria")]
        public IActionResult Criteria()
        {
            //TODO Criteria
            return this.View();
        }

        [HttpGet(Name = "Applications")]
        public IActionResult Applications()
        {
            //TODO Applications
            return this.View();
        }

        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplicationsAsync(int id, CalculateScoresByCriteriaOnCandidateViewModel model)
        {
            var scoresByCriteria = await this.candidatesService.CalculateScoresByCriteria(id);
            //var mother = this.candidatesService.GetCandidateById(id).To;
            return this.View(scoresByCriteria);
        }

        [HttpPost(Name = "AddApplications")]
        public IActionResult AddApplications(ApplicationsListViewModel applicationsListViewModel)
        {
            //TODO Applications
            return this.View();
        }
    }

}