using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class DeleteQuestionModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public DeleteQuestionModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _dbContext.TestQuestions == null)
            {
                return NotFound();
            }
            var thisQuestion = await _dbContext.TestQuestions.FindAsync(id);

            if (thisQuestion != null)
            {
                _dbContext.TestQuestions.Remove(thisQuestion);
                await _dbContext.SaveChangesAsync();
                TempData["ToastMessage"] = "Xoá câu hỏi thành công!";
                return RedirectToPage("/ClassSubjectTests/AddQuestion", new { testId = thisQuestion.TestId });
            }
            TempData["ToastMessage"] = "Câu hỏi không tồn tại!";
            return RedirectToPage("/Index");
        }
    }
}
