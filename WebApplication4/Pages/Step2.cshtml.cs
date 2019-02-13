using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class Step2Model : PageModel
    {
        private readonly AppDbContext _db;
        private SBB _sbb { get; set; }

        public Step2Model(AppDbContext db)
        {
            _db = db;
            _sbb = SBB.Instance("./NaszDzejsonek");
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {


            var customer = _sbb.Get(id);
            if (customer == null)
            {
                return RedirectToPage("/Index", new { id = id });
            }
            Customer = new Customer();
            Customer = Customer.Deserialize(customer);


            //Customer = await _db.Customers.FindAsync(id);
            //if (Customer == null)
            //{
            //    return RedirectToPage("/Index");
            //}
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _sbb.Update<Customer>(Customer.Id, Customer);

            //_db.Attach(Customer).State = EntityState.Modified;

            //await _db.SaveChangesAsync();
            

            return RedirectToPage("./Index", new { id = Customer.Id });
        }


    }
}