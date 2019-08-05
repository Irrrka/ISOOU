namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ISOOU.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class SystemUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public SystemUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Candidates = new HashSet<Candidate>();
            this.Parents = new HashSet<Parent>();
            this.Questions = new HashSet<Question>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public string FullName { get; set; }

        public string UCN { get; set; }

        public SystemRole UserRole { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
