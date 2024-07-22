using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DetailsModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public ClassSubject ClassSubject { get; set; } = default!; 
        public List<ClassSubjectPost> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassSubjects == null)
            {
                return NotFound();
            }
            //get detail info of subject in this class
            var classsubject = await _context.ClassSubjects
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.SubjectTeacherNavigation)
                .FirstOrDefaultAsync(m => m.ClassSubjectId == id);

            if (classsubject == null)
            {
                return NotFound();
            }
            else 
            {
                ClassSubject = classsubject;
                //get list post of this subject in this class
                var listPost = await _context.ClassSubjectPosts.Select(csp => csp)
                    .Where(csp => csp.ClassSubjectId == ClassSubject.ClassSubjectId).ToListAsync();
                if(listPost != null) Posts = listPost;  
            }

            return Page();
        }
                 
    }
}
