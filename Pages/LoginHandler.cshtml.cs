using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SomaShare.Models;

namespace SomaShare.Pages
{
    [IgnoreAntiforgeryToken] // allows form to post without token
    public class LoginHandlerModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginHandlerModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string Email, string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(
                Email,
                Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Login worked — go to dashboard
                return Redirect("/dashboard");
            }
            else
            {
                // Login failed — go back to login page with error message
                return Redirect("/account/login?error=fail");
            }
        }
    }
}
