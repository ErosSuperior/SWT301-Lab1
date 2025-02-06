using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.SignalR;
using ecomerce.Hubs;

namespace ecomerce.Pages.Admin.Products
{
    public class EditModel : PageModel
    {

        private readonly ProductService productService = new ProductService();
        private readonly CategoryService categoryService = new CategoryService();
        private readonly SupplierService supplierService = new SupplierService();
        private readonly IHubContext<SignalHub> _hubContext;
        public EditModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || productService.getProductID(id) == null) return RedirectToPage("/404Page");

            Product = productService.getProductID(id);
            ViewData["CategoryId"] = categoryService.getAllCategory();
            ViewData["SupplierId"] = supplierService.getAllSuppliers();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            ViewData["notice"] = "Update Success";
            await productService.updateProductAsync(Product);
            await _hubContext.Clients.All.SendAsync("ReloadData");
            return Page();
        }

        //private bool ProductExists(int id)
        //{
        //    return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        //}
    }
}
