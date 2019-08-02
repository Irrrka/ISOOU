namespace ISOOU.Data.Models
{
    using System.Collections.Generic;

    public class SystemUser : ApplicationUser
    {
        public SystemUser()
        {
            this.Candidates = new HashSet<Candidate>();
            this.Parents = new HashSet<Parent>();
            this.Questions = new HashSet<Question>();
        }

        public string FullName { get; set; }

        public string UCN { get; set; }

        public SystemRole UserRole { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
