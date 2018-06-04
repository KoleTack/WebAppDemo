using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        private ILogger<CreateModel> m_Log;


        public CreateModel(AppDbContext db, ILogger<CreateModel> log)
        {
            _db = db;
            m_Log = log;
        }

        [TempData]
        public string Msg { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            Msg = $"Customer {Customer.Name} Added!";
            m_Log.LogCritical(Msg);
            return RedirectToPage("/Index");
        }

    }
}