using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace OnlineLearningSystem.Pages.Notifications
{
    public class notificationModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;
        public void OnGet()
        {
        }

        public ContentResult OnGetGetNewNotification()
        {
            var newNoti = _context.Notifications.OrderByDescending(n => n.CreatedDate).FirstOrDefault();
            string jsonStr = JsonSerializer.Serialize(newNoti);
            return Content(jsonStr);
        }
    }
}
