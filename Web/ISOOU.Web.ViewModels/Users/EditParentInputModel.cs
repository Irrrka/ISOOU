using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class EditParentInputModel : IMapFrom<Parent>
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string UCN { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Град по постоянен адрес")]
        public string PermanentCity { get; set; }

        [Required]
        [Display(Name = "Постоянен адрес")]
        public string PermanentAddress { get; set; }

        [Required]
        [Display(Name = "Район по постоянен адрес")]
        public string PermanentDistrict { get; set; }

        [Required]
        [Display(Name = "Град по настоящ адрес")]
        public string CurrentCity { get; set; }

        [Required]
        [Display(Name = "Настоящ адрес")]
        public string CurrentAddress { get; set; }

        [Required]
        [Display(Name = "Район по настоящ адрес")]
        public string CurrentDistrict { get; set; }

        [Display(Name = "Месторабота - име")]
        public string WorkName { get; set; }

        [Display(Name = "Месторабота - район")]
        public string WorkDistrict { get; set; }

    }
}