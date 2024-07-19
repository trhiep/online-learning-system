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
