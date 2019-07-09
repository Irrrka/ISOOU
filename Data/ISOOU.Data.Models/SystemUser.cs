using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Data.Models
{
    public class SystemUser : ApplicationUser
    {
        public SystemUser()
        {
            this.Questions = new HashSet<Question>();
            this.Schools = new List<School>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string UCN { get; set; } //8207116355

        //TODO method to calculate unique number
        public string UniqueNumber => 
            (this.UCN.ToCharArray()[6]* this.UCN.ToCharArray()[7] + this.UCN.ToCharArray()[8] * this.UCN.ToCharArray()[9] + (this.UCN.ToCharArray()[6] + this.UCN.ToCharArray()[7]) + (this.UCN.ToCharArray()[8] + this.UCN.ToCharArray()[9])).ToString();

        public int YearOfBirth { get; set; }

        public string MothersFullName { get; set; }

        public string MothersPhoneNumber { get; set; }

        public string MothersEGN { get; set; }

        public string FathersFullName { get; set; }

        public string FathersPhoneNumber { get; set; }

        public string FathersEGN { get; set; }

        public AddressDetails Address { get; set; }

        public ICollection<School> Schools { get; set; }

        public CandidateStatus Status { get; set; }

        public AdmissionCriteria Criteria { get; set; }

        public int QuestionId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
