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

        public int ChildrenId { get; set; }

        public ICollection<Child> Children { get; set; }

        public int ParentId { get; set; }

        public ICollection<Parent> Parents { get; set; }

        //UserRole-Director
        //public School School { get; set; }

        public int QuestionId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
