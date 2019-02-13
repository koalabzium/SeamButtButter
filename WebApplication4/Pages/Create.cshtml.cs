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


namespace WebApplication4.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly SBB _sbb;

        public CreateModel(AppDbContext db)
        {
            _db = db;
            _sbb = SBB.Instance("NaszDzejsonek.json", 1);
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

            
            //var exists = false;
            //Customers = await _db.Customers.ToListAsync();

            //if (Customers != null)
            //{
            //    foreach (var c in Customers)
            //    {
            //        if (c.Id == Customer.Id)
            //        {
            //            exists = true;
            //            break;
            //        }
            //    }
            //}

            //if (!exists)
            //{
            //    _db.Customers.Add(Customer);
            //    await _db.SaveChangesAsync();
            //}


            return RedirectToPage("/Index", new { id = Customer.Id });
        }

    }
}