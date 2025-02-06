using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Hubs;
using ecomerce.Service;
using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Pages.Admin.Category
{
    public class EditModel : PageModel
    {
        private CategoryService categoryService = new CategoryService();
        private readonly IHubContext<SignalHub> _hubContext;

        public EditModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }


        [BindProperty]
        public Models.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || categoryService.getCategory(id) == null) return RedirectToPage("/404Page");
            Category = categoryService.getCategory(id);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var check =  categoryService.updateCategory(Category);
            if (!check) ViewData["notice"] = "Category already exist";
            else ViewData["notice"] = "Update Success";
            return Page();
        }

    }
}
