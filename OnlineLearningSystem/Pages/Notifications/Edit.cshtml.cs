using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Notifications
{
    public class EditModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public EditModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notification Notification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            checkRole();
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notification =  await _context.Notifications.FirstOrDefaultAsync(m => m.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }
            Notification = notification;
           ViewData["CreatedBy"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            checkRole();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(Notification.NotificationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotificationExists(int id)
        {
          return (_context.Notifications?.Any(e => e.NotificationId == id)).GetValueOrDefault();
        }

        void checkRole()
        {

        }
    }
}
