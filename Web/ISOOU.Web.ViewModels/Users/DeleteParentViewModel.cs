using ISOOU.Data.Models;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class DeleteParentViewModel : IMapFrom<ParentServiceModel>, IMapTo<ParentServiceModel>
    {
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        [Display(Name = "ЕГН")]
        public string UCN { get; set; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Град по постоянен адрес")]
        public string AddressPermanentCity { get; set; }

        [Display(Name = "Постоянен адрес")]
        public string AddressPermanent { get; set; }

        [Display(Name = "Район по постоянен адрес")]
        public string AddressPermanentDistrictName { get; set; }

        [Display(Name = "Град по настоящ адрес")]
        public string AddressCurrentCity { get; set; }

        [Display(Name = "Настоящ адрес")]
        public string AddressCurrent { get; set; }

        [Display(Name = "Район по настоящ адрес")]
        public string AddressCurrentDistrictName { get; set; }

        [Display(Name = "Месторабота - име")]
        public string WorkName { get; set; }

        [Display(Name = "Месторабота - район")]
        public string WorkDistrictName { get; set; }

        public string ParentRole { get; set; }

        public string UserName { get; set; }
    }
}