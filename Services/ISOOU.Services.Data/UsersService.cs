namespace ISOOU.Services.Data
{
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IRepository<SystemUser> userRepository;
        private readonly IRepository<Question> qestionRepository;

        public UsersService(IRepository<SystemUser> userRepository, IRepository<Question> qestionRepository)
        {
            this.userRepository = userRepository;
            this.qestionRepository = qestionRepository;
        }

        public async Task<bool> CreateMessage(string userIdentity, QuestionServiceModel model)
        {
            SystemUser user = await this.userRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.Email == userIdentity);

            var quetsion = new Question
            {
                Subject = model.Subject,
                Content = model.Content,
                SystemUserId = user.Id == null ? null : user.Id,
            };

            await this.qestionRepository.AddAsync(quetsion);
            var result = await this.qestionRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<SystemUserServiceModel> GetUserByuserName(string userIdentity)
        {
            SystemUserServiceModel user = (await this.userRepository
                           .All()
                           .FirstOrDefaultAsync(x => x.UserName == userIdentity)).To<SystemUserServiceModel>();

            return user;

        }
    }
}
