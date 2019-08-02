//namespace ISOOU.Services.Data
//{
//    using System;
//    using System.Security.Claims;
//    using System.Threading.Tasks;

//    using ISOOU.Data.Common.Repositories;
//    using ISOOU.Data.Models;
//    using ISOOU.Services.Data.Contracts;
//    using ISOOU.Services.Mapping;
//    using ISOOU.Web.ViewModels.Home;
//    using ISOOU.Web.ViewModels.Users;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.EntityFrameworkCore;

//    public class UsersService : IUsersService
//    {
//        private readonly UserManager<SystemUser> userManager;
//        private readonly IRepository<Question> questionsRepository;
//        private readonly IRepository<Candidate> childrenRepository;
//        private readonly IRepository<Parent> parentsRepository;

//        public UsersService(
//            UserManager<SystemUser> userManager,
//            IRepository<Question> questionsRepository,
//            IRepository<Candidate> childrenRepository,
//            IRepository<Parent> parentsRepository)
//        {
//            this.userManager = userManager;
//            this.questionsRepository = questionsRepository;
//            this.childrenRepository = childrenRepository;
//            this.parentsRepository = parentsRepository;
//        }

//        public ClaimsPrincipal User { get; private set; }

//        public async Task<ChildViewModel> AddChildToUserAsync(ChildInputModel model)
//        {
//            var user = await this.userManager.GetUserAsync(this.User);

//            if (user == null)
//            {
//                throw new NullReferenceException();
//            }

//            var child = new Candidate
//            {
//                FirstName = model.FirstName,
//                MiddleName = model.MiddleName,
//                LastName = model.LastName,
//                UCN = model.UCN,
//                User = user,
//                YearOfBirth = model.YearOfBirth,
//            };

//            await this.childrenRepository.AddAsync(child);
//            await this.childrenRepository.SaveChangesAsync();

//            var childViewModel = await this.childrenRepository.All()
//                                                   .To<ChildViewModel>()
//                                                    .FirstOrDefaultAsync(p => p.UCN == model.UCN);

//            if (childViewModel == null)
//            {
//                throw new NullReferenceException();
//            }

//            return childViewModel;
//        }

//        public async Task<ParentViewModel> AddParentToUserAsync(ParentInputModel model)
//        {
//            var user = await this.userManager.GetUserAsync(this.User);

//            if (user == null)
//            {
//                throw new NullReferenceException();
//            }

//            var parent = new Parent
//            {
//                FirstName = model.FirstName,
//                MiddleName = model.MiddleName,
//                LastName = model.LastName,
//                UCN = model.UCN,
//                User = user,
//                Address = new AddressDetails
//                {
//                    Permanent = model.AddressPermanet,
//                    Current = model.AddressCurrent,
//                },
//                WorkName = model.NameOfWork,
//                PhoneNumber = model.PhoneNumber,
//            };

//            await this.parentsRepository.AddAsync(parent);
//            await this.parentsRepository.SaveChangesAsync();

//            var parentViewModel = await this.parentsRepository.All()
//                                                   .To<ParentViewModel>()
//                                                    .FirstOrDefaultAsync(p => p.UCN == model.UCN);
//            if (parentViewModel == null)
//            {
//                throw new NullReferenceException();
//            }

//            return parentViewModel;
//        }

//        public async Task<bool> CreateMessage(ContactFormInputModel model)
//        {
//            //var user = await this.userManager.GetUserAsync(this.User);
//            //if (user == null)
//            //{
//            //    return false;
//            //}

//            //var question = new Question
//            //{
//            //    Subject = model.Subject,
//            //    Content = model.Content,
//            //    User = user,
//            //};

//            //await this.questionsRepository.AddAsync(question);
//            //await this.questionsRepository.SaveChangesAsync();

//            return true;
//        }


//    }
//}
