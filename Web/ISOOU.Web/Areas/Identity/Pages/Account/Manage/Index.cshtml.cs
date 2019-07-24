namespace ISOOU.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ISOOU.Data.Common.Repositories;
    using ISOOU.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

#pragma warning disable SA1649 // File name should match first type name
    public class IndexModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly UserManager<SystemUser> userManager;
        private readonly IRepository<SystemUser> userRepository;

        public IndexModel(
            UserManager<SystemUser> userManager,
            IRepository<SystemUser> userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            List<Parent> parents = (await this.userRepository.All().ToListAsync())
                .FirstOrDefault(u => u.UserName == user.UserName)
                .Parents
                .ToList();

            List<Child> children = (await this.userRepository.All().ToListAsync())
               .FirstOrDefault(u => u.UserName == user.UserName)
               .Children
               .ToList();

            var model = new InputModel
            {
                Children = children,
                Parents = parents,
            };

            return this.Page();
        }


        public class InputModel
        {
            public List<Child> Children { get; set; }

            public List<Parent> Parents { get; set; }
        }
    }
}
