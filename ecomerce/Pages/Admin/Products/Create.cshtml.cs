using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Service;
using ecomerce.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private ProductService productService = new ProductService();
        private CategoryService categoryService = new CategoryService();
        private SupplierService supplierService = new SupplierService();
        private readonly IHubContext<SignalHub> _hubContext;
        [BindProperty]
        public Product Product { get; set; } = default!;
        public CreateModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = categoryService.getAllCategory();
            ViewData["SupplierId"] = supplierService.getAllSuppliers();
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            await productService.insertProductAsync(Product);
            await _hubContext.Clients.All.SendAsync("ReloadData");
            return RedirectToPage("/Admin/Products/Index");
        }
    }
}
