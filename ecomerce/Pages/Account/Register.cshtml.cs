using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ecomerce.Models;
using ecomerce.Service;
using ecomerce.IService;

namespace ecomerce.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IMailKitService _mailKitService;
        private readonly IAccountService _accountService;

        public RegisterModel(IMailKitService mailKitService, IAccountService accountService)
        {
            _mailKitService = mailKitService;
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ecomerce.Models.Account Account { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (_accountService.CheckAccountDuplicate(Account)) return Page();
            await _accountService.RegisterAccountAsync(Account);

            string email = Account.UserName;
            string name = Account.FullName;
            string message = $"Cảm ơn {name} đã đăng ký dịch vụ của chúng tôi";
            
            await _mailKitService.SendEmailAsync(email, "Confirm Register", message);

            return RedirectToPage("/Account/Login");
        }
    }
}
