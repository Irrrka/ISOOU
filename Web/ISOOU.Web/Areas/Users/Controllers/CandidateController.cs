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
            return this.View();
        }

        [HttpPost("/Users/Candidate/Create")]
        public async Task<IActionResult> Create(CreateCandidateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string userName = this.User.Identity.Name;

            var candidateToAdd = new CandidateServiceModel();
            var userSM = this.User.To<SystemUserServiceModel>();
            candidateToAdd.SEN = input.SEN == "ДА" ? true : false;
            candidateToAdd.Desease = input.Desease == "ДА" ? true : false;
            candidateToAdd.FirstName = input.FirstName;
            candidateToAdd.MiddleName = input.MiddleName;
            candidateToAdd.LastName = input.LastName;
            candidateToAdd.KinderGarten = input.KinderGarten;
            candidateToAdd.UCN = input.UCN;
            candidateToAdd.YearOfBirth = input.YearOfBirth;
            candidateToAdd.User.UserName = userSM.UserName;
            //MotherAndFather
         //   this.User.FindFirst(ClaimTypes.NameIdentifier).Value) !!!!!!!!!!!!
            //candidateToAdd = input.To<CandidateServiceModel>();

            await this.candidatesService.Create(candidateToAdd);

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

        [HttpPut("/Users/Candidate/Edit")]
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
        public IActionResult Criteria(int id)
        {
            //TODO Criteria
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
