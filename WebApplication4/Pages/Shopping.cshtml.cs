using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication4.Pages
{
    public class ShoppingModel : PageModel
    {
        private readonly AppDbContext _db;

        public ShoppingModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        public List<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int handler)
        {
            Customer = await _db.Customers.FindAsync(handler);
            if (Customer == null)
            {
                return RedirectToPage("/Index", new { id = handler });
            }
            Products = Customer.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index", new { id = Customer.Id });
        }



    }
}