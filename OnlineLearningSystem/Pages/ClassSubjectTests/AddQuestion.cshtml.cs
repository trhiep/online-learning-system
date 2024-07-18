using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class AddQuestionModel : PageModel
    {
        private OLS_DBContext _dbContext;

        public AddQuestionModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public IList<TestQuestion> TestQuestions { get; set; } = default!;

        public IActionResult OnGet(int? testId)
        {
            if (testId == null)
            {
                return RedirectToPage("/ClassSubjects/Index");
            }

            TestQuestions = _dbContext.TestQuestions.Where(x => x.TestId == testId).ToList();
            ViewData["testId"] = testId;

            return Page();
        }
    }
}
