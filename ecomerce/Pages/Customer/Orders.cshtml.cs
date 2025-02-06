using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Service;
using System.Text.Json;

namespace ecomerce.Pages.Customer
{
    public class IndexModel : PageModel
    {
      
        private OrderService orderService = new OrderService();
        public IList<Order> Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {

            var customer = HttpContext.Session.GetString("customer");
            if (customer == null) return RedirectToPage("/404Page");
            var id = JsonSerializer.Deserialize<Models.Account>(customer).AccountId;
            Order = orderService.getOrderCustomer(id);
            return Page();

        }
    }
}
