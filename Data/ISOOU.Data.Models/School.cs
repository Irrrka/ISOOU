﻿namespace ISOOU.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ISOOU.Data.Common.Models;

    public class School : BaseModel<int>
    {
        public School()
        {
            this.Candidates = new HashSet<CandidateApplication>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public int DistrictId { get; set; }

        [Required]
        public virtual District District { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [DataType(DataType.Url)]
        public string URLOfSchool { get; set; }

        [DataType(DataType.Url)]
        public string URLOfMap { get; set; }

        public int FreeSpots { get; set; }

        public virtual ICollection<CandidateApplication> Candidates { get; set; }
    }
}