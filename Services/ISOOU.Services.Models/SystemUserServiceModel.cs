using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using Microsoft.AspNetCore.Identity;

namespace ISOOU.Services.Models
{
    public class SystemUserServiceModel : IdentityUser, IMapFrom<SystemUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}