using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactApp.Pages
{
    public class ContactModel : PageModel
    {
        //create form variables
        private readonly ContactStore _store;

        [BindProperty]
        public Contact Form { get; set; }
        public ContactModel(ContactStore store) => _store = store;


        //form logic to load
        public void OnGet() { /*display empty form */ }


        //form logic to post
        public IActionResult OnPost()
        {
            //validate model
            if (!ModelState.IsValid)
            {
                return Page();
            }



            //add submission to store
            Form.SubmittedUtc = DateTime.UtcNow;
            _store.Add(Form);

            //redirect to thank you page
            return RedirectToPage("/ThankYou");
        }
    }
}
