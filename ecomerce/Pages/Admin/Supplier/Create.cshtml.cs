using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ecomerce.Models;
using ecomerce.Hubs;
using ecomerce.Service;
using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Pages.Admin.Supplier
{
    public class CreateModel : PageModel
    {
        private SupplierService supplierService = new SupplierService();
        private readonly IHubContext<SignalHub> _hubContext;


        public CreateModel(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Supplier Supplier { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var check = supplierService.insertSupplier(Supplier);
            if (!check) ViewData["notice"] = "Supplier already exist";
            else ViewData["notice"] = "Supplier Success";

            return RedirectToPage("/Admin/Supplier/Index");
        }
    }
}
