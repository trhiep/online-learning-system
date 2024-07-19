using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLearningSystem.Pages.Logins
{
    public class LogoutModel : PageModel
    {

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                HttpContext.Session.Clear();
                // Điều hướng về trang chính hoặc trang đăng nhập
                return Page();
            }
            return Page();
            // Xóa session





        }
    }
}
