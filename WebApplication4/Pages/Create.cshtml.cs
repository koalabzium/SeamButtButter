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

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        
        [BindProperty]
        public Customer Customer { get; set; }

        public IList<Customer> Customers { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {

            //Customer = Biblioteka.serialize(Customer)
            //Biblioteka.add(id, Customer)

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            //formatter.Serialize(stream, Customer);
            //stream.Close();

            //stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            //Customer = (Customer)formatter.Deserialize(stream);
            //stream.Close();

            var ser = Customer.Serialize();
            SBB mojabiblioteka = new SBB("./NaszDzejsonek.json");
            mojabiblioteka.Add(Customer.Id, ser);
            //SBB.add(ser, Customer.






            var exists = false;
            Customers = await _db.Customers.ToListAsync();

            if (Customers != null)
            {
                foreach (var c in Customers)
                {
                    if (c.Id == Customer.Id)
                    {
                        exists = true;
                        break;
                    }
                }
            }

            if (!exists)
            {
                _db.Customers.Add(Customer);
                await _db.SaveChangesAsync();
            }






            return RedirectToPage("/Index", new { id = Customer.Id });
        }

    }
}