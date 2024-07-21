using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils.EmailUtils;

namespace OnlineLearningSystem.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;
        private readonly IHubContext<SignalRHub> _hubContext;

        public CreateModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            checkRole();
            LoadReciver();
            return Page();
        }

        [BindProperty]
        public Notification Notification { get; set; } = default!;
        public IList<Account> Accounts { get; set; }
        public IList<Models.Classroom> Classrooms { get; set; } = default!;
        public IList<ClassStudent> ClassStudents { get; set; }
        [BindProperty]
        public List<int> sendTo {  get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (Notification == null)
            {
                return Page();
            }
            checkRole();
            Notification.CreatedBy = 4;
            DateTime date= DateTime.Now;
            Notification.CreatedDate = date;
            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();
            Notification n = _context.Notifications.Where(n => n.CreatedDate.CompareTo(date) == 0).FirstOrDefault();

            foreach (var id in sendTo)
            {
                Account acc = _context.Accounts.Find(id);
                if (acc != null)
                {
                    NotificationOfAccount noti = new NotificationOfAccount();
                    noti.To = id;
                    noti.IsRead = false;
                    _context.NotificationOfAccounts.Add(noti);
                    EmailSender email = new EmailSender();
                    email.SendEmail(acc.Email, "New notification: " + n.Title, n.Content);
                }
            }
            //await _hubContext.Clients.All.SendAsync("NewNotification");
            return RedirectToPage("./Index");
        }
        void LoadReciver()
        {
            Accounts = _context.Accounts.ToList();
            Classrooms = _context.Classrooms.ToList();
            ClassStudents = _context.ClassStudents.ToList();
        }
        void checkRole()
        {

        }
    }
}
