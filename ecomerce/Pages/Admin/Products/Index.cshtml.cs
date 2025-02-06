using ecomerce.Hubs;
using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private ProductService productService = new ProductService();
        public IList<Product> Products { get; set; } = default!;
        private CategoryService categoryService = new CategoryService();
        private SupplierService supplierService = new SupplierService();
        private readonly IHubContext<SignalHub> _hubContext;
        [BindProperty]
        public Product Product { get; set; } = default!;
        public IndexModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OnGetAsync()
        {
            Products = await productService.getAllProductsAsync();
            ViewData["CategoryId"] = categoryService.getAllCategory();
            ViewData["SupplierId"] = supplierService.getAllSuppliers();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await productService.insertProductAsync(Product);
            await _hubContext.Clients.All.SendAsync("ReloadData");
            return RedirectToPage("/Admin/Products/Index");
        }
    }
}
