using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Gen
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DetailsModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

      public Subject Subject { get; set; } = default!; 
        public IList<ClassSubject> ClassSubjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }
            else 
            {
                Subject = subject;
                GetClassBySubject(subject.SubjectId);
            }
            return Page();
        }
        void GetClassBySubject(int id)
        {
            ClassSubjects = _context.ClassSubjects.Include("Class").Where(c => c.SubjectId == id).ToList();
        }
    }
}
