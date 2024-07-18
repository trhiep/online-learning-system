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

        public void OnGet()
        {
        }

		public IActionResult OnPostAsync()
		{
			Account loginAccount = oLS_DBContext.Accounts.Where
				(x => x.Username == Acc.Username && x.Password == Acc.Password).FirstOrDefault();
			if (loginAccount != null) {

				HttpContext.Session.SetString("UserSession", loginAccount.Username);
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
