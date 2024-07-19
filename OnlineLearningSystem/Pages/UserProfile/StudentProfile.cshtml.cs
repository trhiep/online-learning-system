using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLearningSystem.Pages.UserProfile
{
    public class StudentProfileModel : PageModel
    {
        public IActionResult OnGet()
        {
            Console.WriteLine(string.IsNullOrEmpty(HttpContext.Session.GetString("RoleSession")) && HttpContext.Session.GetString("RoleSession") == "Teacher");
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("RoleSession")) && HttpContext.Session.GetString("RoleSession") == "Teacher")
            {
                TempData["ErrorRole"] = "Login with admin to access";
                return RedirectToPage("/Error");
            }

            // Người dùng đã đăng nhập, tiếp tục xử lý
            return Page();
        }
    }
}
