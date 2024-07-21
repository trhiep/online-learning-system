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
        public IList<NotificationOfAccount> notificationOfAccounts{ get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string role = HttpContext.Session.GetString("RoleSession");
            string user = HttpContext.Session.GetString("UserSession");
            if (role==null)
            {
                return RedirectToPage("../Authen/Login");
            }
            Account account = _context.Accounts.Where(a => a.Username.Equals(user)).FirstOrDefault();
            if (_context.NotificationOfAccounts != null)
            {
                if (role.Equals("Admin"))
                {
                    int id = account.AccountId;
                    notificationOfAccounts = await _context.NotificationOfAccounts
                    .Include(n => n.Notification).Where(n => n.Notification.CreatedBy==id).OrderByDescending(n => n.NotificationId)
                    .ToListAsync();
                }
                else
                {
                    notificationOfAccounts = await _context.NotificationOfAccounts
                    .Include(n => n.Notification).Where(n=>n.To.Equals(account.AccountId)).OrderByDescending(n => n.NotificationId)
                    .ToListAsync();
                }
            }
            return Page();
        }
    }
}
