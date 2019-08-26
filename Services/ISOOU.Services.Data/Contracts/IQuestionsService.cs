namespace ISOOU.Services.Data.Contracts
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface IQuestionsService
    {
        Task<bool> CreateMessage(ClaimsPrincipal userIdentity, QuestionServiceModel model);

        Task<QuestionServiceModel> ReadLastMessage();
    }
}
