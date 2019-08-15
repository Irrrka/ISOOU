using AutoMapper;
using ISOOU.Data.Models;
using ISOOU.Data.Models.Enums;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Users
{
    public class EditParentInputModel : IMapFrom<ParentServiceModel>, IMapTo<ParentServiceModel>, IHaveCustomMappings
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

        public string FullName { get; set; }

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

        public int AddressId { get; set; }

        [Display(Name = "Месторабота - име")]
        public string WorkName { get; set; }

        [Display(Name = "Месторабота - район")]
        public string WorkDistrictName { get; set; }

        public int WorkDistrictId { get; set; }

        public string ParentRole { get; set; }

        public string UserName { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<ParentServiceModel, EditParentInputModel>()
               .ForMember(
                    destination => destination.AddressCurrent,
                    opts => opts.MapFrom(origin => origin.Address.Current))
                .ForMember(
                    destination => destination.AddressPermanent,
                    opts => opts.MapFrom(origin => origin.Address.Permanent))
              .ForMember(
                    destination => destination.AddressCurrentCity,
                    opts => opts.MapFrom(origin => origin.Address.CurrentCity))
              .ForMember(
                    destination => destination.AddressPermanentCity,
                    opts => opts.MapFrom(origin => origin.Address.PermanentCity))
              .ForMember(
                    destination => destination.AddressCurrentDistrictName,
                    opts => opts.MapFrom(origin => origin.Address.CurrentDistrict.Name))
              .ForMember(
                    destination => destination.AddressCurrentDistrictName,
                    opts => opts.MapFrom(origin => origin.Address.PermanentDistrict.Name));

            configuration
               .CreateMap<CreateParentInputModel, ParentServiceModel>()
               .ForMember(
                    destination => destination.Address,
                    opts => opts.MapFrom(origin => new AddressDetailsServiceModel
                    {
                        Current = origin.AddressCurrent,
                        Permanent = origin.AddressPermanent,
                        CurrentDistrict = new DistrictServiceModel
                        {
                            Name = origin.AddressCurrentDistrictName
                        },
                        PermanentDistrict = new DistrictServiceModel
                        {
                            Name = origin.AddressPermanentDistrictName
                        },
                        CurrentCity = (CityName)Enum.Parse(typeof(CityName), origin.AddressCurrentCity),
                        PermanentCity = (CityName)Enum.Parse(typeof(CityName), origin.AddressPermanentCity),
                    }))
                .ForMember(
                    destination => destination.WorkDistrict,
                    opts => opts.MapFrom(origin => new DistrictServiceModel
                        {
                            Name = origin.WorkDistrictName
                        }));
        }
    }
}