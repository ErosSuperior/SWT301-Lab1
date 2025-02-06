using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Service;
using ecomerce.IService;

namespace ecomerce.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IMailKitService _mailKitService;
        private AccountService accountService = new AccountService();

        public RegisterModel(IMailKitService mailKitService)
        {
            _mailKitService = mailKitService;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ecomerce.Models.Account Account { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (accountService.checkAccountDuplicate(Account)) return Page();
            accountService.registerAccountAsync(Account);
            string email = Account.UserName;
            string name = Account.FullName;
            string message = "Cam on " + name + " da dang ky dich vu cua chung toi";
            await _mailKitService.SendEmailAsync(email, "Confirm Register", message);
            return RedirectToPage("/Account/Login");
        }
    }
}
