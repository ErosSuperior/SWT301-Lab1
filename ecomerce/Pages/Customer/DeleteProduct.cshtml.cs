using ecomerce.Hubs;
using ecomerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ecomerce.Pages.Customer
{
    public class DeleteProductModel : PageModel
    {
        private OrderService orderService = new OrderService();
        private readonly IHubContext<SignalHub> _hubContext;
        public DeleteProductModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = HttpContext.Session.GetString("customer");
            if (customer == null) return RedirectToPage("/404Page");
            int cusid = JsonSerializer.Deserialize<Models.Account>(customer).AccountId;
            orderService.removeItemInCart(cusid, id);
            await _hubContext.Clients.All.SendAsync("ReloadData");
            return RedirectToPage("/Customer/Orders");
        }
    }
}
