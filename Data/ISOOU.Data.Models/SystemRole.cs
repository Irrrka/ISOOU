using ISOOU.Data.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace ISOOU.Data.Models
{
    public class SystemRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public SystemRole()
           : this(null)
        {
        }

        public SystemRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
