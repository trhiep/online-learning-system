using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class EditModel : PageModel
    {
        OLS_DBContext _dbContext;
        public Account LogedInAccount;

        public EditModel(OLS_DBContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public ClassSubjectTest ClassSubjectTest { get; set; } = default!;
        [BindProperty]
        public string TestStartDate { get; set; } = default!;

        [BindProperty]
        public string TestEndDate { get; set; } = default!;
        public IActionResult OnGet(int? testId)
        {
            if (testId != null)
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
                {
                    string username = HttpContext.Session.GetString("UserSession");
                    LogedInAccount = _dbContext.Accounts.Where(x => x.Username == username).First();
                    if (LogedInAccount != null && LogedInAccount.Role == "Teacher")
                    {
                        ClassSubjectTest = _dbContext.ClassSubjectTests.Where(x => x.TestId == testId).First();
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            var thisTest = _dbContext.ClassSubjectTests.Where(x => x.TestId == ClassSubjectTest.TestId).First();
            if (thisTest != null)
            {
                thisTest.TestName = ClassSubjectTest.TestName;
                thisTest.TestDescription = ClassSubjectTest.TestDescription;
                thisTest.Attempts = ClassSubjectTest.Attempts;
                thisTest.Duration = ClassSubjectTest.Duration;
                thisTest.StartDate = ClassSubjectTest.StartDate;
                thisTest.EndDate = ClassSubjectTest.EndDate;
                thisTest.PassScore = ClassSubjectTest.PassScore;

                _dbContext.ClassSubjectTests.Update(thisTest);
                _dbContext.SaveChanges();
                return RedirectToPage("/ClassSubjectTests/TestDetails", new { testId = ClassSubjectTest.TestId });
            }
            return RedirectToPage("/Index");
        }
    }
}
