using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace WebApplication4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IList<Customer> Customers { get; private set; }
        public Customer Customer { get; set; }

        private readonly SBB _sbb;

        public IndexModel(AppDbContext db)
        {
            _db = db;
            _sbb = SBB.Instance("./NaszDzejsonek.json",1);

        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var customer = _sbb.Get(id);
            if (customer == null)
            {
                return RedirectToPage("/Create");
            }
            Customer = new Customer();
            Customer = Customer.Deserialize(customer);
            //Customers = await _db.Customers.ToListAsync();
            //Customer = await _db.Customers.FindAsync(id);
            if (Customer == null)
            {
                return RedirectToPage("/Create");
            }
            return Page();
            
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //var customer = await _db.Customers.FindAsync(id);

            //if (customer != null)
            //{
            //    _db.Customers.Remove(customer);
            //    await _db.SaveChangesAsync();
            //}

            _sbb.Delete(id);

            return RedirectToPage();
        }
    }
}
