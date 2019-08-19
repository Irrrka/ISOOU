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
        private readonly ICalculatorService calculatorService;
        private readonly ISchoolsService schoolsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICriteriasService criteriasService;
        private readonly IAdminService adminService;

        public CandidateController(
            UserManager<SystemUser> userManager,
            IParentsService parentsService,
            ICandidatesService candidatesService,
            ISchoolsService schoolsService,
            ICloudinaryService cloudinaryService,
            ICalculatorService calculatorService,
            ICriteriasService criteriasService,
            IAdminService adminService)
        {
            this.userManager = userManager;
            this.parentsService = parentsService;
            this.candidatesService = candidatesService;
            this.calculatorService = calculatorService;
            this.schoolsService = schoolsService;
            this.cloudinaryService = cloudinaryService;
            this.criteriasService = criteriasService;
            this.adminService = adminService;
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

        [HttpGet("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = (await this.candidatesService.GetCandidateById(id))
                                    .To<EditCandidateInputModel>();

            if (model == null)
            {
                return this.Redirect("/");
            }

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

            return this.View(model);
        }

        [HttpPost("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(int id, EditCandidateInputModel input)
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

            //Radio tag helper
            ClaimsPrincipal userIdentity = this.User;

            CandidateServiceModel candidatToEdit = input.To<CandidateServiceModel>();
            int motherId = await this.parentsService.GetParentIdByFullName(userIdentity, input.MotherFullName);
            int fatherId = await this.parentsService.GetParentIdByFullName(userIdentity, input.FatherFullName);
            candidatToEdit.MotherId = motherId;
            candidatToEdit.FatherId = fatherId;

            await this.candidatesService.Edit(id, userIdentity, candidatToEdit);

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

        [HttpGet]
        public IActionResult Documents(int id, UploadDocumentsInputModel input)
        {
            input.Id = id;
            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Documents(UploadDocumentsInputModel input)
        {
            string applicationUrl = await this.cloudinaryService
                .UploadDocument(input.Application, nameof(input.Application));
            //string birthCertificateUrl = await this.cloudinaryService
                //.UploadDocument(input.BirthCertificate, nameof(input.BirthCertificate));
            return this.Redirect("/");
        }

        [HttpGet(Name = "Criteria")]
        public async Task<IActionResult> Criteria(int id)
        {
            ScoresByCriteriasOnCandidateViewModel model =
                new ScoresByCriteriasOnCandidateViewModel();
            model.CandidateId = id;
            model.ScoresByCriteria = new List<ScoreByCriteriaOnCandidateViewModel>();

            IEnumerable<CriteriaForCandidateServiceModel> criteriasOfCandidate = await this.criteriasService.GetCriteriasAndScoresByCandidateId(id);

            IEnumerable<CriteriaServiceModel> allcriterias = await this.criteriasService.GetAllCriterias();


            foreach (var criteriaOfCandidate in criteriasOfCandidate)
            {
                ScoreByCriteriaOnCandidateViewModel criteriaModel =
                new ScoreByCriteriaOnCandidateViewModel();

                criteriaModel.CriteriaDisplayName = criteriaOfCandidate.Criteria.DisplayName;
                criteriaModel.CriteriaScores = criteriaOfCandidate.Criteria.Scores;
                if (criteriaOfCandidate.Sch != 0)
                {
                    criteriaModel.SchoolName =
                        criteriaOfCandidate.Candidate.Applications
                        .FirstOrDefault(sch => sch.SchoolId == criteriaOfCandidate.Sch).School.Name;
                }

                model.ScoresByCriteria.Add(criteriaModel);
            }

            return this.View(model);
        }

        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(int id)
        {
            CandidateServiceModel candidate = await this.candidatesService.GetCandidateById(id);

            var allSchools = this.schoolsService
                .GetAllSchools()
                .Where(x => x.District.Id == candidate.Mother.Address.PermanentDistrictId
                    || x.District.Id == candidate.Mother.Address.CurrentDistrictId
                    || x.District.Id == candidate.Father.Address.PermanentDistrictId
                    || x.District.Id == candidate.Father.Address.CurrentDistrictId
                    || x.District.Id == candidate.Mother.WorkDistrictId
                    || x.District.Id == candidate.Father.WorkDistrictId);

            this.ViewData["AllSchools"] = allSchools
                .Select(p => new AddSchoolApplicationsViewModel { Id = p.Id, Name = p.Name, DistrictName = p.District.Name })
                .ToList();


            AddApplicationsInputModel model = new AddApplicationsInputModel();
            model.CandidateId = candidate.Id;

            return this.View("AddApplications", model);
        }

        [HttpPost(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(int id, AddApplicationsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
               return this.View("AddAplications");
            }

            ClaimsPrincipal userIdentity = this.User;
            List<int> schoolApplicationIds = new List<int>();

            //TODO Refactor Scalability
            #region refactorWithServiceModel
            //candidate.SchoolCandidates.Add(
            //    new SchoolCandidateServiceModel
            //    {
            //        CandidateId = id,
            //        SchoolId = firstWishSchoolId,
            //    });
            #endregion

            //First wish
            int firstWishSchoolId = await this.schoolsService
                                            .GetSchoolIdByName(input.FirstWishSchool);
            schoolApplicationIds.Add(firstWishSchoolId);

            //Second wish
            int secondWishSchoolId = await this.schoolsService
                                            .GetSchoolIdByName(input.SecondWishSchool);
            schoolApplicationIds.Add(secondWishSchoolId);

            //Third wish
            int thirdWishSchoolId = await this.schoolsService
                                            .GetSchoolIdByName(input.ThirdWishSchool);
            schoolApplicationIds.Add(thirdWishSchoolId);

            await this.candidatesService.AddApplications(id, schoolApplicationIds);

            return this.Redirect($"/Users/Candidate/Profile/{id}");
        }

        [HttpGet(Name = "Profile")]
        public async Task<IActionResult> Profile(int id)
        {
            CandidateServiceModel candidate = await this.candidatesService.GetCandidateById(id);
            var procedureStatus = this.adminService.GetProcedureStatus();

            CandidateProfileViewModel model = new CandidateProfileViewModel();
            model.CandidateId = id;
            model.CandidateName = candidate.FirstName;
            model.CandidateStatus = candidate.Status.ToString();
            model.ProcedureStatus = procedureStatus;

            int basicScores = candidate.BasicScores;
            int totalScores = 0;

            foreach (var schApp in candidate.Applications)
            {
                int additionalScoresForApplication = schApp.AdditionalScoresForSchool;
                totalScores = basicScores + additionalScoresForApplication;
                model.ScoresByApplications.Add(schApp.School.Name, totalScores);
            }

            var sortedApplications = model.ScoresByApplications.OrderByDescending(x=>x.Value);
            model.ScoresByApplications = sortedApplications.ToDictionary(x=>x.Key, y=>y.Value);

            return this.View(model);
        }
    }
}
