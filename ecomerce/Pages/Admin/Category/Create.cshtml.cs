using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.SignalR;
using ecomerce.Hubs;

namespace ecomerce.Pages.Admin.Category
{
    public class CreateModel : PageModel
    {

        private CategoryService categoryService = new CategoryService();
        private readonly IHubContext<SignalHub> _hubContext;


        public CreateModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Category Category { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            var check = categoryService.InsertCategory(Category);
            if (!check) ViewData["notice"] = "Category already exist";
            else ViewData["notice"] = "Create Success";
            return Page();
        }
    }
}
