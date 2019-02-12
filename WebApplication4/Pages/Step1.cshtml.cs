using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class Step1Model : PageModel
    {
        private readonly AppDbContext _db;

        public Step1Model(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int handler)
        {
            Customer = await _db.Customers.FindAsync(handler);
            if (Customer == null)
            {
                return RedirectToPage("/Index", new { id = handler });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Customer {Customer.Id} not found", e);
            }

            return RedirectToPage("./Step2", new { id = Customer.Id });
        }
    }
}