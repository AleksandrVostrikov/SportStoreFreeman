using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SportStoreFreeman.Pages.Admin
{
    [Authorize]
    public class IdentityUsersModel : PageModel
    {
        public IdentityUser AdminUser { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        public IdentityUsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            AdminUser = await _userManager.FindByNameAsync("Drevont");
        }
    }
}
