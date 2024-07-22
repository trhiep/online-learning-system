using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassStudents
{
    public class IndexModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public IndexModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IList<ClassStudent> ClassStudent { get;set; } = default!;

        public async Task OnGetAsync(int? classId)
        {
            var ClassId = (int)classId;

            if (_context.ClassStudents != null)
            {
                ClassStudent = await _context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Where(c => c.ClassId == ClassId)
                .ToListAsync();
            }
        }
    }
}
