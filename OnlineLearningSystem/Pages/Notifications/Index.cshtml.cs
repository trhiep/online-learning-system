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
    public class IndexModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public IndexModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IList<Notification> Notification { get; set; } = default!;

        public async Task OnGetAsync()
        {
            checkRole();
            if (_context.Notifications != null)
            {
                Notification = await _context.Notifications
                .Include(n => n.CreatedByNavigation).OrderByDescending(n => n.NotificationId).ToListAsync();
            }
        }
        void checkRole()
        {

        }
    }
}
