namespace ISOOU.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ISOOU.Data.Common.Models;

    public class School : BaseModel<int>
    {
        public School()
        {
            this.Classes = new HashSet<Class>();
            this.Candidates = new HashSet<Candidates_Schools>();
        }

        public string Name { get; set; }

        public virtual AddressDetails Address { get; set; }

        public virtual District District { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public virtual ICollection<Candidates_Schools> Candidates { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public int FreePlaces { get; set; }

        public virtual AdmissionProcedure AdmissionProcedure { get; set; }
    }
}