using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Service;

namespace ecomerce.Pages.Admin.Category
{
    public class CreateModel : PageModel
    {
        private CategoryService categoryService = new CategoryService();

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnPost()
        {
            var check = categoryService.InsertCategory(Category);
            if (!check) ViewData["notice"] = "Category already exist";
            else ViewData["notice"] = "Create Success";
            return Page();
        }
    }
}
