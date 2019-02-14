using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConversationManager.SeamButBetter;

namespace ConversationManager.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Customer> Customers { get; private set; }
        public Customer Customer { get; set; }

        private readonly SBB _sbb;

        public IndexModel()
        {
            _sbb = SBB.Instance(new FileDriver("./JsonFile.json",1));

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
            
            if (Customer == null)
            {
                return RedirectToPage("/Create");
            }
            return Page();
            
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _sbb.Delete(id);
            return RedirectToPage();
        }
    }
}
