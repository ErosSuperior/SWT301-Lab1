using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Service;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;
using ecomerce.Hubs;

namespace ecomerce.Pages
{
    public class DetailProductModel : PageModel
    {
        private ProductService productService = new ProductService();
        private OrderService orderService = new OrderService();
        private readonly IHubContext<SignalHub> _hubContext;
        public DetailProductModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public Product Product { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || productService.getProductID(id) == null) return RedirectToPage("/404Page");

            Product = productService.getProductID(id);
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int quantity, int productID)
        {
            if (quantity == null) return Page();
            var customer = HttpContext.Session.GetString("customer");
            if (customer == null) return RedirectToPage("/Account/Login");

            var acc = JsonSerializer.Deserialize<Models.Account>(customer);

            Order od = orderService.getCartOrder(acc.AccountId);
            if (od != null) orderService.addToCart(productID, od.OrderId, quantity);
            else orderService.addNewCart(acc.AccountId, productID, quantity, acc.Address);

            //_hubContext.Clients.All.SendAsync("ReloadData");
            return RedirectToPage("/Customer/Cart");
        }
    }
}
