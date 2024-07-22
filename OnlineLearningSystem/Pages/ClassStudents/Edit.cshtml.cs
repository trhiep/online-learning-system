using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassStudents
{
    public class EditModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public EditModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassStudent ClassStudent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassStudents == null)
            {
                return NotFound();
            }

            var classstudent =  await _context.ClassStudents.FirstOrDefaultAsync(m => m.ClassStudentId == id);
            if (classstudent == null)
            {
                return NotFound();
            }
            ClassStudent = classstudent;
           ViewData["ClassId"] = new SelectList(_context.Classrooms, "ClassId", "ClassId");
           ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
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

            _context.Attach(ClassStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassStudentExists(ClassStudent.ClassStudentId))
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

        private bool ClassStudentExists(int id)
        {
          return (_context.ClassStudents?.Any(e => e.ClassStudentId == id)).GetValueOrDefault();
        }
    }
}
