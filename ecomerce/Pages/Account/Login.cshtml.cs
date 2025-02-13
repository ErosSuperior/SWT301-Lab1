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
        public required string capCha { get; set; }

        [BindProperty]
        public string validate { get; set; } = "";

        [BindProperty]
        public required string Username { get; set; } // Added 'required' modifier

        [BindProperty]
        public required string Password { get; set; } // Added 'required' modifier

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
                // Attempt to get the account based on username and password
                var acc = await Task.Run(() => _accountService.getAccount(Username, Password));

                if (acc == null)
                {
                    setNotice("Wrong Username or Password");
                    return Page();
                }

                // Set the session based on user type (staff or customer)
                var sessionStr = acc.Type ? "staff" : "customer";
                var accJson = JsonSerializer.Serialize(acc);
                HttpContext.Session.SetString(sessionStr, accJson);

                // Redirect to homepage after successful login
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Log the error with additional contextual information (username)
                _logger.LogError(ex, "An error occurred during the login process for username: {Username}", Username);

                // Set a user-friendly notice
                setNotice("An error occurred while processing your login. Please try again later.");

                // Optionally, rethrow the exception with additional context for debugging
                throw new InvalidOperationException("An error occurred while attempting to log in.", ex);
            }
        }

        public void setNotice(string notice)
        {
            // Generate a new random string for the captcha
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
