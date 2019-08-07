namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using ISOOU.Services.Models;

    public interface IUsersService
    {
        Task<bool> CreateMessage(string userIdentity, QuestionServiceModel model);
    }
}
