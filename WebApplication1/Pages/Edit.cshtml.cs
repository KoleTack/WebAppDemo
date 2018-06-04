using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db) { _db = db; }

        [TempData]
        public string Msg { get; set; }
        
        [BindProperty]
        public Customer customer { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                //Adress is missing
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(customer).State = EntityState.Modified;
            Msg = $"Customer {customer.Name} has been updated!";

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException E)
            {
                throw new Exception($"Customer {customer.ID} not found!", E);
            }

            return RedirectToPage("./Index");

        }



    }
}