using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConversationManager.SeamButBetter;

namespace ConversationManager.Pages
{
    public class Step1Model : PageModel
    {
        private readonly SBB _sbb;

        public Step1Model()
        {
            _sbb = SBB.Instance(new FileDriver("./JsonFile"));
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int handler)
        {

            var customer = _sbb.Get(handler);
            if (customer == null)
            {
                return RedirectToPage("/Index", new { id = handler });
            }
            Customer = new Customer();
            Customer = Customer.Deserialize(customer);

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

            //try
            //{
            //    await _db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException e)
            //{
            //    throw new Exception($"Customer {Customer.Id} not found", e);
            //}

            return RedirectToPage("./Step2", new { id = Customer.Id });
        }
    }
}