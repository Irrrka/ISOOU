using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class EditParentInputModel : IMapFrom<ParentServiceModel>, IMapTo<ParentServiceModel>
    {
        public int Id { get; set; }

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

       
        [Display(Name = "ЕГН")]
        public string UCN { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Град по постоянен адрес")]
        public string AddressPermanentCity { get; set; }

        [Required]
        [Display(Name = "Постоянен адрес")]
        public string AddressPermanent { get; set; }

        [Required]
        [Display(Name = "Район по постоянен адрес")]
        public string AddressPermanentDistrictName { get; set; }


        [Required]
        [Display(Name = "Град по настоящ адрес")]
        public string AddressCurrentCity { get; set; }

        [Required]
        [Display(Name = "Настоящ адрес")]
        public string AddressCurrent { get; set; }

        [Required]
        [Display(Name = "Район по настоящ адрес")]
        public string AddressCurrentDistrictName { get; set; }

        [Display(Name = "Месторабота - име")]
        public string WorkName { get; set; }

        [Display(Name = "Месторабота - район")]
        public string WorkDistrictName { get; set; }

        public string ParentRole { get; set; }

        public string UserName { get; set; }

        public int AddressId { get; set; }
    }
}