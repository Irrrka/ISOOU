namespace ISOOU.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ISOOU.Data.Common.Models;

    [NotMapped]
    public abstract class Person : BaseModel<int>, IDeletableEntity
    {
        //TODO Person with GuidId!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        [Required]
        [StringLength(10)]
        public string UCN { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual SystemUser User { get; set; }

        public bool IsDeleted { get ; set ; }

        public DateTime? DeletedOn { get ; set; }
    }
}
