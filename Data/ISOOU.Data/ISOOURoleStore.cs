namespace ISOOU.Data
{
    using System.Security.Claims;

    using ISOOU.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ISOOURoleStore : RoleStore<
        ApplicationRole,
        ISOOUContext,
        string,
        IdentityUserRole<string>,
        IdentityRoleClaim<string>>
    {
        public ISOOURoleStore(ISOOUContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(ApplicationRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
