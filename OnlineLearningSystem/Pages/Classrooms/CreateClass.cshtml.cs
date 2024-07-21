using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Classrooms
{
    public class CreateClassModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;


        public CreateClassModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            //ViewData["CreateBy"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            //ViewData["FormTeacherId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            return Page();
        }
        [BindProperty]
        public Classroom Classroom { get; set; } = default!;



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid data" });
            }

            _context.Classrooms.Add(Classroom);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
    }
}
