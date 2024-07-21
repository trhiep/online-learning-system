using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.ViewModel;

namespace OnlineLearningSystem.Pages.Logins
{
    public class LoginModel : PageModel
    {
		private readonly OLS_DBContext oLS_DBContext;

		public LoginModel(OLS_DBContext oLS_DBContext)
        {
			this.oLS_DBContext = oLS_DBContext;
		}

        [BindProperty]
        public Account Acc { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                return RedirectToPage("/Index");
            }

            // Người dùng đã đăng nhập, tiếp tục xử lý
            return Page();
        }

		public IActionResult OnPostAsync()
		{
			Account loginAccount = oLS_DBContext.Accounts.Where
				(x => x.Username == Acc.Username && x.Password == Acc.Password).FirstOrDefault();
			if (loginAccount != null) {

				HttpContext.Session.SetString("UserSession", loginAccount.Username);
                HttpContext.Session.SetString("RoleSession", loginAccount.Role);
                HttpContext.Session.SetString("AccountIDSession", loginAccount.AccountId.ToString());

                return RedirectToPage("/Index");
            }
			else
			{
				ErrorMessage = "Username Or Email Doesn't Match. Try Again!!";

                return Page();
			}

		}

       

    }
}
