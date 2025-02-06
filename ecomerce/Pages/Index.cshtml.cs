using ecomerce.Hubs;
using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Pages
{
    public class IndexModel : PageModel
    {
        private ProductService productService = new ProductService();
        public IList<Product> Product { get; set; } = default!;
        private readonly IHubContext<SignalHub> _hubContext;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;
        public IndexModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OnGetAsync(string? strSearch, double? firstInput, double? secondInput, int? pageNum)
        {
            var pageNumber = pageNum ?? 1;
            CurrentPage = pageNumber;

            var allProducts = await productService.getProductsAsync(strSearch, firstInput, secondInput);
            var totalCount = allProducts.Count;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var products = allProducts.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
            Product = products;

            ViewData["strSearch"] = strSearch;
            ViewData["firstInput"] = firstInput;
            ViewData["secondInput"] = secondInput;
        }
    }
}