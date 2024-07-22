using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.SubjectPost
{
    public class EditPostModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public EditPostModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassSubjectPost ClassSubjectPost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassSubjectPosts == null)
            {
                return NotFound();
            }

            var classsubjectpost =  await _context.ClassSubjectPosts.FirstOrDefaultAsync(m => m.PostId == id);
            if (classsubjectpost == null)
            {
                return NotFound();
            }
            ClassSubjectPost = classsubjectpost;
           ViewData["ClassSubjectId"] = new SelectList(_context.ClassSubjects, "ClassSubjectId", "ClassSubjectId");
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

            _context.Attach(ClassSubjectPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectPostExists(ClassSubjectPost.PostId))
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

        private bool ClassSubjectPostExists(int id)
        {
          return (_context.ClassSubjectPosts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
