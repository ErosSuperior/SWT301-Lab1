using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecomerce.Models;
using ecomerce.Service;
using ecomerce.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace ecomerce.Pages.Admin.Supplier
{
    public class IndexModel : PageModel
    {

        private SupplierService supplierService = new SupplierService();

        public IList<Models.Supplier> Suppliers { get; set; } = default!;
        [BindProperty]
        public Models.Supplier Supplier { get; set; } = default!;
        private readonly IHubContext<SignalHub> _hubContext;


        public IndexModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OnGetAsync()
        {
            Suppliers = supplierService.getAllSupplier();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var check = supplierService.insertSupplier(Supplier);
            if (!check) ViewData["notice"] = "Supplier already exist";
            else ViewData["notice"] = "Supplier Success";

            return RedirectToPage("/Admin/Supplier/Index");
        }
    }
}
