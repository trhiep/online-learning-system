using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Classrooms
{
    public class EditModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public EditModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Classroom Classroom { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom =  await _context.Classrooms.FirstOrDefaultAsync(m => m.ClassId == id);
            if (classroom == null)
            {
                return NotFound();
            }
            Classroom = classroom;
           ViewData["CreateBy"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
           ViewData["FormTeacherId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
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

            _context.Attach(Classroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(Classroom.ClassId))
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

        private bool ClassroomExists(int id)
        {
          return (_context.Classrooms?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}
