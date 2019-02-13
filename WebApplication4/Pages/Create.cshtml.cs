using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.SeamButBetter;

namespace WebApplication4.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SBB _sbb;

        public CreateModel()
        {
            _sbb = SBB.Instance(new FileDriver("NaszDzejsonek.json", 1));
        }


        
        [BindProperty]
        public Customer Customer { get; set; }

        public IList<Customer> Customers { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _sbb.Add(Customer.Id, Customer);

            //SBB.add(ser, Customer.

            return RedirectToPage("/Index", new { id = Customer.Id });
        }

    }
}