namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels.Home;
    using ISOOU.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> CreateMessage(ContactFormInputModel model);

        //Task<ParentViewModel> AddParentToUserAsync(ParentInputModel parent);

        //Task<ChildViewModel> AddChildToUserAsync(ChildInputModel child);
    }
}
