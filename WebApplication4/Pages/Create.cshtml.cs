using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConversationManager.SeamButBetter;

namespace ConversationManager.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SBB _sbb;

        public CreateModel()
        {
            _sbb = SBB.Instance(new FileDriver("JsonFile.json", 1));
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

            return RedirectToPage("/Index", new { id = Customer.Id });
        }

    }
}