using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassStudents
{
    public class CreateModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public CreateModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClassId"] = new SelectList(_context.Classrooms, "ClassId", "ClassId");
        ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            return Page();
        }

        [BindProperty]
        public ClassStudent ClassStudent { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ClassStudents == null || ClassStudent == null)
            {
                return Page();
            }

            _context.ClassStudents.Add(ClassStudent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
