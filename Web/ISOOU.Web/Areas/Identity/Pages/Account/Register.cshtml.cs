namespace ISOOU.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using ISOOU.Common;
    using ISOOU.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<SystemUser> signInManager;
        private readonly RoleManager<SystemRole> roleManager;
        private readonly UserManager<SystemUser> userManager;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<SystemUser> userManager,
            SignInManager<SystemUser> signInManager,
            RoleManager<SystemRole> roleManager,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("/");
            if (this.ModelState.IsValid)
            {
                var isRoot = !this.userManager.Users.Any();
                var user = new SystemUser { UserName = this.Input.Email, Email = this.Input.Email };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    if (isRoot)
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                    }
                    else
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                    }

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Е-поща")]
            public string Email { get; set; }

            [DataType(DataType.EmailAddress)]
            [Display(Name = "Повтори е-поща")]
            [Compare("Email", ErrorMessage = "Двете е-пощи не съвпадат.")]
            public string ConfirmEmail { get; set; }

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
}
