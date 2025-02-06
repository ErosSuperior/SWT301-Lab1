using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ecomerce.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AccountService _accountService;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string capCha { get; set; }

        [BindProperty]
        public string validate { get; set; } = "";

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(AccountService accountService, ILogger<LoginModel> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public void OnGet()
        {
            validate = GenerateRandomString();
            capCha = string.Join(" ", validate.ToCharArray());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                setNotice("Input all the fields");
                return Page();
            }

            try
            {
                var acc =  _accountService.getAccount(Username, Password);

                if (acc == null)
                {
                    setNotice("Wrong Username or Password");
                    return Page();
                }

                var sessionStr = acc.Type ? "staff" : "customer";
                var accJson = JsonSerializer.Serialize(acc);
                HttpContext.Session.SetString(sessionStr, accJson);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in OnPostAsync");
                throw;
            }
        }

        public void setNotice(string notice)
        {
            validate = GenerateRandomString();
            capCha = string.Join(" ", validate.ToCharArray());
            ViewData["notice"] = notice;
        }

        public static string GenerateRandomString()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                resultBuilder.Append(chars[rnd.Next(chars.Length)]);
            }
            string result = resultBuilder.ToString();
            return result;
        }
    }
}
