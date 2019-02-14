using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConversationManager.SeamButBetter;

namespace ConversationManager.Pages
{
    public class Step2Model : PageModel
    {
        private SBB _sbb { get; set; }

        public Step2Model()
        {
            _sbb = SBB.Instance(new FileDriver("./NaszDzejsonek"));
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