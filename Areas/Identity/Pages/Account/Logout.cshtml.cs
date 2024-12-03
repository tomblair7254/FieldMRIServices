using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldMRIServicesServices.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Redirect to the login page
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}