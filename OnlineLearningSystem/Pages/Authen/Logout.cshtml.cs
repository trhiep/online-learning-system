using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLearningSystem.Pages.Logins
{
    public class LogoutModel : PageModel
    {

        public IActionResult OnGet()
        {

            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.Clear();
            // Điều hướng về trang chính hoặc trang đăng nhập
            return RedirectToPage("/Authen/Login");

            // Xóa session





        }
    }
}
