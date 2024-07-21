using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Notifications
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DetailsModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public Notification Notification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            checkRole();
            if (id == null || _context.Notifications == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications.Include(n => n.CreatedByNavigation).FirstOrDefaultAsync(m => m.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }
            else
            {
                Notification = notification;
            }
            return Page();
        }
        void checkRole()
        {

        }
    }
}
