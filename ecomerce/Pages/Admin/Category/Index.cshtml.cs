using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.SignalR;
using ecomerce.Hubs;
namespace ecomerce.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {

        private CategoryService categoryService = new CategoryService();
        private readonly IHubContext<SignalHub> _hubContext;
        public IndexModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public IList<Models.Category> Categories { get; set; } = default!;
        [BindProperty]
        public Models.Category Category { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Categories = categoryService.getAllCategories();
        }
        public async Task<IActionResult> OnPost()
        {
            var check = categoryService.InsertCategory(Category);
            if (!check) ViewData["notice"] = "Category already exist";
            else ViewData["notice"] = "Create Success";
            return RedirectToPage("/Admin/Category/Index");
        }
    }
}
