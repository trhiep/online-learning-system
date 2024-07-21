using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.SubjectPost
{
    public class DetailPostModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DetailPostModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

      public ClassSubjectPost ClassSubjectPost { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassSubjectPosts == null)
            {
                return NotFound();
            }

            var classsubjectpost = await _context.ClassSubjectPosts.FirstOrDefaultAsync(m => m.PostId == id);
            if (classsubjectpost == null)
            {
                return NotFound();
            }
            else 
            {
                ClassSubjectPost = classsubjectpost;
            }
            return Page();
        }
    }
}
