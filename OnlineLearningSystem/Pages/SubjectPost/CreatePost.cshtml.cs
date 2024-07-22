using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.SubjectPost
{
    public class CreatePostModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public CreatePostModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? classSubjectId)
        {
            ViewData["ClassSubjectId"] = classSubjectId;
            return Page();
        }

        [BindProperty]
        public ClassSubjectPost Post { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ClassSubjectPosts == null || Post == null)
            {
                return Page();
            }

            Post.PostedDate = DateTime.Now;

            _context.ClassSubjectPosts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
