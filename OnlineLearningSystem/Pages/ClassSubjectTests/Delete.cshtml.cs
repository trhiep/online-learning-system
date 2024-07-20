using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class DeleteModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public DeleteModel(OLS_DBContext dbContext)
        { 
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _dbContext.ClassSubjectTests == null)
            {
                return NotFound();
            }
            var thisTest = await _dbContext.ClassSubjectTests.FindAsync(id);

            if (thisTest != null)
            {
                _dbContext.ClassSubjectTests.Remove(thisTest);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToPage("./MyPosts");
        }
    }
}
