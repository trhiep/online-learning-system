using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class DeleteModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DeleteModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassSubjects == null)
            {
                return NotFound();
            }

            var classsubject = await _context.ClassSubjects.FindAsync(id);

            if (classsubject != null)
            {
                _context.ClassSubjects.Remove(classsubject);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ListClassSubject");
        }
    }
}
