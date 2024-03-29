﻿namespace ISOOU.Web.Areas.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using ISOOU.Web.Areas.Users.Controllers;
    using ISOOU.Web.ViewModels.Schools;
    using ISOOU.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    //TODO cache CandidateId, CandidateName
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
            ClaimsPrincipal userIdentity = this.User;

            var parents = this.parentsService.GetParents(userIdentity);

            if (parents.ToList().Count <= 1)
            {
                return this.BadRequest(GlobalConstants.TwoParents);
            }

            if (parents.FirstOrDefault().User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var motherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Майка);

            var motherList = new List<string>
            {
                $"{motherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

            var fatherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Баща);

            var fatherList = new List<string>
            {
                $"{fatherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
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
                    ParentRole.Няма.ToString(),
                 };

                var fatherFullName = await this.parentsService
                 .GetParentFullNameByRole(this.User, ParentRole.Баща);

                var fatherList = new List<string>
                {
                    $"{fatherFullName}",
                    ParentRole.Друг.ToString(),
                    ParentRole.Няма.ToString(),
                };

                this.ViewData["Mother"] = motherList;
                this.ViewData["Father"] = fatherList;

                return this.View(input);
            }

            ClaimsPrincipal userIdentity = this.User;

            var parents = this.parentsService.GetParentsWithOtherAndNull(userIdentity);

            ParentServiceModel motherServiceModel = await parents.Where(p => p.FullName.TrimEnd().Equals(input.MotherFullName)).FirstOrDefaultAsync();
            ParentServiceModel fatherServiceModel = await parents.Where(p => p.FullName.TrimEnd().Equals(input.FatherFullName)).FirstOrDefaultAsync();

            CandidateServiceModel model = input.To<CandidateServiceModel>();

            model.MotherId = motherServiceModel.Id;
            model.FatherId = fatherServiceModel.Id;

            await this.candidatesService.Create(userIdentity, model);

            var candidatesOfMother = this.candidatesService.GetCandidatesOfParent(userIdentity, model.MotherId).ToList();
            var candidatesOfFather = this.candidatesService.GetCandidatesOfParent(userIdentity, model.FatherId).ToList();

            if (candidatesOfFather.Count >= GlobalConstants.ChildrenInFamily)
            {
                foreach (var candidate in candidatesOfFather)
                {
                    await this.calculatorService.EditBasicScoresByCriteria(candidate.Id);
                }
            }

            if (candidatesOfMother.Count >= GlobalConstants.ChildrenInFamily)
            {
                foreach (var candidate in candidatesOfMother)
                {
                    await this.calculatorService.EditBasicScoresByCriteria(candidate.Id);
                }
            }

            return this.Redirect("/");
        }

        [HttpGet("/Users/Candidate/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var motherFullName = await this.parentsService
            .GetParentFullNameByRole(this.User, ParentRole.Майка);

            var motherList = new List<string>
            {
                $"{motherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

            var fatherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Баща);

            var fatherList = new List<string>
            {
                $"{fatherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

            this.ViewData["Mother"] = motherList;
            this.ViewData["Father"] = fatherList;

            var model = candidate.To<EditCandidateInputModel>();
            model.CandidateName = candidate.FullName;
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
                ParentRole.Няма.ToString(),
            };

                var fatherFullName = await this.parentsService
                 .GetParentFullNameByRole(this.User, ParentRole.Баща);

                var fatherList = new List<string>
            {
                $"{fatherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

                this.ViewData["Mother"] = motherList;
                this.ViewData["Father"] = fatherList;

                return this.View(input);
            }

            ClaimsPrincipal userIdentity = this.User;

            CandidateServiceModel candidatToEdit = input.To<CandidateServiceModel>();
            int motherId = await this.parentsService.GetParentIdByFullName(userIdentity, input.MotherFullName);
            int fatherId = await this.parentsService.GetParentIdByFullName(userIdentity, input.FatherFullName);
            candidatToEdit.MotherId = motherId;
            candidatToEdit.FatherId = fatherId;

            await this.candidatesService.Edit(id, userIdentity, candidatToEdit);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var model = (await this.candidatesService.GetCandidateById(id))
                .To<DeleteCandidateViewModel>();

            if (model == null)
            {
                return this.Redirect($"Edit/{id}");
            }

            var motherFullName = await this.parentsService
           .GetParentFullNameByRole(this.User, ParentRole.Майка);

            var motherList = new List<string>
            {
                $"{motherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

            var fatherFullName = await this.parentsService
             .GetParentFullNameByRole(this.User, ParentRole.Баща);

            var fatherList = new List<string>
            {
                $"{fatherFullName}",
                ParentRole.Друг.ToString(),
                ParentRole.Няма.ToString(),
            };

            this.ViewData["Mother"] = motherList;
            this.ViewData["Father"] = fatherList;
            model.CandidateName = candidate.FullName;
            return this.View(model);
        }

        [HttpPost]
        [Route("/Users/Candidate/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"/Users/Candidate/Edit/{id}");
            }

            await this.candidatesService.Delete(id);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Documents(int id, CreateDocumentInputModel input)
        {
            input.CandidateId = id;
            var candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            if (candidate.Applications.Count == 0)
            {
                return this.BadRequest(GlobalConstants.ApplicationsNotFound);
            }
            input.CandidateName = candidate.FullName;
            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Documents(CreateDocumentInputModel input)
        {
            if (input.Application == null)
            {
                return this.NotFound(GlobalConstants.DocumentNotFound);
            }

            if (!input.Application.FileName.Contains(".pdf"))
            {
                return this.BadRequest(GlobalConstants.DocumentNotSupport);
            }

            await this.candidatesService.CreateDocument(input);

            return this.Redirect("/");
        }

        [HttpGet(Name = "Criteria")]
        public async Task<IActionResult> Criteria(int id)
        {
            CandidateServiceModel candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            ScoresByCriteriasOnCandidateViewModel model =
                new ScoresByCriteriasOnCandidateViewModel();
            model.CandidateId = id;
            model.ScoresByCriteria = new List<ScoreByCriteriaOnCandidateViewModel>();
            var schoolsCriteria = new List<ScoreByCriteriaOnCandidateViewModel>();
            var basicCriteria = new List<ScoreByCriteriaOnCandidateViewModel>();

            IEnumerable<CriteriaForCandidateServiceModel> criteriasOfCandidate = await this.criteriasService.GetCriteriasAndScoresByCandidateId(id);

            IEnumerable<CriteriaServiceModel> allcriterias = await this.criteriasService.GetAllCriterias();


            foreach (var criteriaOfCandidate in criteriasOfCandidate)
            {
                ScoreByCriteriaOnCandidateViewModel criteriaModel =
                                  new ScoreByCriteriaOnCandidateViewModel();

                if (criteriaOfCandidate.Sch == 0)
                {
                    criteriaModel.CriteriaDisplayName = criteriaOfCandidate.Criteria.DisplayName;
                    criteriaModel.CriteriaScores = criteriaOfCandidate.Criteria.Scores;
                    basicCriteria.Add(criteriaModel);
                }
                else if (criteriaOfCandidate.Sch != 0)
                {
                    var appIds = this.candidatesService.GetCandidateApplications(candidate.Id);
                    foreach (var appId in appIds)
                    {
                        if (appId == criteriaOfCandidate.Sch)
                        {
                            criteriaModel.CriteriaDisplayName = criteriaOfCandidate.Criteria.DisplayName;
                            criteriaModel.CriteriaScores = criteriaOfCandidate.Criteria.Scores;
                            criteriaModel.SchoolName =
                                (await this.schoolsService.GetSchoolDetailsById(criteriaOfCandidate.Sch)).Name;
                            schoolsCriteria.Add(criteriaModel);
                        }
                    }
                }
            }

            model.ScoresByCriteria.AddRange(basicCriteria);
            schoolsCriteria.Distinct();
            model.ScoresByCriteria.AddRange(schoolsCriteria);
            model.CandidateName = candidate.FullName;
            return this.View(model);
        }

        [HttpGet(Name = "AddApplications")]
        public async Task<IActionResult> AddApplications(int id)
        {
            CandidateServiceModel candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            IQueryable<SchoolServiceModel> allSchools = null;

            if (candidate.MotherId == 1 || candidate.MotherId == 2
                || candidate.FatherId == 1 || candidate.FatherId == 2)
            {
                allSchools = this.schoolsService
                .GetAllSchools();
            }
            else
            {
                allSchools = this.schoolsService
                .GetAllSchools()
                .Where(x => x.District.Id == candidate.Mother.Address.PermanentDistrictId
                    || x.District.Id == candidate.Mother.Address.CurrentDistrictId
                    || x.District.Id == candidate.Father.Address.PermanentDistrictId
                    || x.District.Id == candidate.Father.Address.CurrentDistrictId
                    || x.District.Id == candidate.Mother.WorkDistrictId
                    || x.District.Id == candidate.Father.WorkDistrictId);
            }

            this.ViewData["AllSchools"] = allSchools
                .Select(p => new AddSchoolApplicationsViewModel { Id = p.Id, Name = p.Name, DistrictName = p.District.Name })
                .ToList();

            AddApplicationsInputModel model = new AddApplicationsInputModel();
            model.CandidateId = candidate.Id;
            model.CandidateName = candidate.FullName;
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
            if (input.FirstWishSchool != null)
            {
                int firstWishSchoolId = await this.schoolsService
                                           .GetSchoolIdByName(input.FirstWishSchool);
                if (!schoolApplicationIds.Contains(firstWishSchoolId))
                {
                    schoolApplicationIds.Add(firstWishSchoolId);
                }
            }

            //Second wish
            if (input.SecondWishSchool != null)
            {
                int secondWishSchoolId = await this.schoolsService
                                            .GetSchoolIdByName(input.SecondWishSchool);
                if (!schoolApplicationIds.Contains(secondWishSchoolId))
                {
                    schoolApplicationIds.Add(secondWishSchoolId);
                }
            }

            //Third wish
            if (input.ThirdWishSchool != null)
            {
                int thirdWishSchoolId = await this.schoolsService
                                            .GetSchoolIdByName(input.ThirdWishSchool);
                if (!schoolApplicationIds.Contains(thirdWishSchoolId))
                {
                    schoolApplicationIds.Add(thirdWishSchoolId);
                }
            }

            await this.candidatesService.AddApplications(id, schoolApplicationIds);

            return this.Redirect($"/Users/Candidate/Profile/{id}");
        }

        [HttpGet(Name = "Profile")]
        public async Task<IActionResult> Profile(int id)
        {
            CandidateServiceModel candidate = await this.candidatesService.GetCandidateById(id);

            if (candidate == null || candidate.User.UserName != this.User.Identity.Name)
            {
                return this.View("_AccessDenied");
            }

            var procedureStatus = (await this.adminService.GetLastProcedure()).Status.ToString();

            CandidateProfileViewModel model = new CandidateProfileViewModel();
            model.CandidateId = id;
            model.CandidateName = candidate.FirstName;
            model.CandidateStatus = candidate.Status.ToString();
            model.ProcedureStatus = procedureStatus;

            int basicScores = candidate.BasicScores;
            int totalScores = 0;

            foreach (var schApp in candidate.Applications)
            {
                var schoolName = (await this.schoolsService.GetSchoolDetailsById(schApp.SchoolId)).Name;
                int additionalScoresForApplication = schApp.AdditionalScoresForSchool;
                totalScores = basicScores + additionalScoresForApplication;
                model.ScoresByApplications.Add(schoolName, totalScores);
            }

            var sortedApplications = model.ScoresByApplications.OrderByDescending(x => x.Value);
            model.ScoresByApplications = sortedApplications.ToDictionary(x => x.Key, y => y.Value);
            model.CandidateName = candidate.FullName;
            return this.View(model);
        }
    }
}
