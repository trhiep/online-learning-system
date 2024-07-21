using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class EditModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public EditModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassSubject ClassSubject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassSubjects == null)
            {
                return NotFound();
            }

            var classsubject =  await _context.ClassSubjects.FirstOrDefaultAsync(m => m.ClassSubjectId == id);
            if (classsubject == null)
            {
                return NotFound();
            }
            ClassSubject = classsubject;
           ViewData["ClassId"] = new SelectList(_context.Classrooms, "ClassId", "ClassId");
           ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
           ViewData["SubjectTeacher"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ClassSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectExists(ClassSubject.ClassSubjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClassSubjectExists(int id)
        {
          return (_context.ClassSubjects?.Any(e => e.ClassSubjectId == id)).GetValueOrDefault();
        }
    }
}
