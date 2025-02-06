using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ecomerce.Pages.Customer
{
    public class CartModel : PageModel
    {

        private OrderService orderService = new OrderService();
        public IList<OrderDetail> OrderDetail { get; set; } = default!;
        public decimal total = 0;
        public async Task<IActionResult> OnGetAsync()
        {
            var customer = HttpContext.Session.GetString("customer");
            if (customer == null) return RedirectToPage("/404Page");
            var id = JsonSerializer.Deserialize<Models.Account>(customer).AccountId;
            OrderDetail = orderService.getCart(id);
            total = (decimal)OrderDetail.Sum(od => od.Price * od.Quantity);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(DateTime requireDate)
        {
            var customer = HttpContext.Session.GetString("customer");
            if (customer == null) return RedirectToPage("/404Page");
            var id = JsonSerializer.Deserialize<Models.Account>(customer).AccountId;
            orderService.startOrder(id, requireDate);
            return RedirectToPage("/Customer/Orders");
        }
    }
}
