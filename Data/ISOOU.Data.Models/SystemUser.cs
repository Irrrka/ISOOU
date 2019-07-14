namespace ISOOU.Data.Models
{
    using System.Collections.Generic;

    public class SystemUser : ApplicationUser
    {
        public SystemUser()
        {
            this.Children = new HashSet<Child>();
            this.Parents = new HashSet<Parent>();
            this.Questions = new HashSet<Question>();
        }

        public virtual ICollection<Child> Children { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
