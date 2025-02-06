using ecomerce.Models;
using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecomerce.Pages.Customer
{
    public class OrderDetailsModel : PageModel
    {
        private OrderService orderService = new OrderService();
        public IList<OrderDetail> OrderDetail { get; set; } = default!;
        public Order Order { get; set; } = default!;
        public decimal total = 0;
        public async Task<IActionResult> OnGetAsync(int id)
        {
if (id == null)
{
    return RedirectToPage("/404Page");
}
else
{
    Order = orderService.getOrder(id);
    OrderDetail = orderService.getOrderDetails(id);
    total = (decimal)OrderDetail.Sum(od => od.Price * od.Quantity);
    return Page();
}
        }
    }
}
