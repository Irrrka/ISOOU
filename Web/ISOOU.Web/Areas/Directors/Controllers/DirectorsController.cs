namespace ISOOU.Web.Areas.Administration.Controllers
{
    using ISOOU.Common;
    using ISOOU.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.DirectorRoleName)]
    [Area("Directors")]
    public class DirectorsController : BaseController
    {
    }
}
