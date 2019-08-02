namespace ISOOU.Web.Areas.Users.Controllers
{
    using ISOOU.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(nameof(Users))]
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class UserController : Controller
    {
    }
}
