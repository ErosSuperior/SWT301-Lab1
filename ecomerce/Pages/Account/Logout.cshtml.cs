using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecomerce.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.Remove("staff");
            HttpContext.Session.Remove("customer");

            return RedirectToPage("/Index");
        }
    }
}
