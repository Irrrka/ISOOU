namespace ISOOU.Services.Data
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class QuestionsService : IQuestionsService
    {
        private readonly IRepository<Question> qestionRepository;
        private readonly UserManager<SystemUser> userManager;

        public QuestionsService(UserManager<SystemUser> userManager, IRepository<Question> qestionRepository)
        {
            this.userManager = userManager;
            this.qestionRepository = qestionRepository;
        }

        public async Task<bool> CreateMessage(ClaimsPrincipal userIdentity, QuestionServiceModel model)
        {
            var quetsion = new Question
            {
                Subject = model.Subject,
                Content = model.Content,
                SystemUserId = model.SystemUserId,
            };

            await this.qestionRepository.AddAsync(quetsion);
            var result = await this.qestionRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<QuestionServiceModel> ReadLastMessage()
        {
            var message = await this.qestionRepository
                .All()
                .Include(u => u.User)
                .OrderByDescending(x => x.CreatedOn)
                .To<QuestionServiceModel>()
                .FirstOrDefaultAsync();

            return message;
        }
    }
}
