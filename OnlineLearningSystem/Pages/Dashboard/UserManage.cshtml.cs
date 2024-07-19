using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLearningSystem.Pages.Dashboard
{
    public class UserManageModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("RoleSession")) && HttpContext.Session.GetString("RoleSession") !="Admin")
            {
                TempData["ErrorRole"] = "Login with admin to access";
                return RedirectToPage("/Error");
            }

            // Người dùng đã đăng nhập, tiếp tục xử lý
            return Page();
        }
    }
}
