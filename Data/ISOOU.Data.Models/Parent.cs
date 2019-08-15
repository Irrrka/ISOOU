namespace ISOOU.Data.Models
{
    using ISOOU.Data.Models.Enums;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Parent : Person
    {
        public Parent()
            : base()
        {
            //this.CandidateParents = new HashSet<CandidateParent>();
        }

        public ParentRole Role { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public virtual AddressDetails Address { get; set; }

        public string WorkName { get; set; }

        public int WorkDistrictId { get; set; }

        public virtual District WorkDistrict { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

    }
}
