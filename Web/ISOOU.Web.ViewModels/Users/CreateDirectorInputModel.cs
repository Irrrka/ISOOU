﻿namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDirectorInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string UCN { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Е-поща")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтори парола")]
        [Compare("Password", ErrorMessage = "Двете пароли не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}