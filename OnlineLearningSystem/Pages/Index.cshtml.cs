using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Utils.EmailUtils;

namespace OnlineLearningSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private bool IsUserAuthenticated()
        {
            var account = SE1728_Group2_A2.Utils.SessionHelper.SessionExtensions.GetObjectFromJson<Staff>(HttpContext.Session, "Staff");
            if (account != null)
            {
                if (account.Role == 1)
                {
                    return true;
                }
            }

            return false;
        }


        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                return RedirectToPage("Logins/Login");
            }

            // Người dùng đã đăng nhập, tiếp tục xử lý
            return Page();
        }
    }
}
