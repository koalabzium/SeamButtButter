using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public IList<Product> Products { get; private set; }
        public Customer Customer { get; set; }
        private readonly AppDbContext _db;
      

        public ShoppingCartModel(AppDbContext db)
        {
            _db = db;
        }

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

        //public async Task<IActionResult> OnPostKupaAsync(int id)
        //{
        //    var product = await _db.Products.FindAsync(id);

        //    if (product != null)
        //    {
        //        _db.Products.Remove(product);
        //        await _db.SaveChangesAsync();
        //    }

        //    return RedirectToPage();
        //}
    }
}