using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class DeleteModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DeleteModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? classID)
        {
            if (id == null || classID == null || _context.ClassSubjects == null)
            {
                return NotFound();
            }

            var classsubject = await _context.ClassSubjects.FindAsync(id);

            if (classsubject != null)
            {
                _context.ClassSubjects.Remove(classsubject);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ListClassSubject", new { id = classID });
        }
    }
}
