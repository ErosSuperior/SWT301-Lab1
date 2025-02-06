using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Service;

namespace ecomerce.Pages.Admin.Products
{
    public class DetailsModel : PageModel
    {
        private ProductService productService = new ProductService();



        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || productService.getProductID(id) == null) return RedirectToPage("/404Page");
            Product = productService.getProductID(id);
            return Page();
        }
    }
}
