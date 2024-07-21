using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;
using Org.BouncyCastle.Utilities;

namespace OnlineLearningSystem.Pages.Gen
{
    public class IndexModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public IndexModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get; set; } = default!;
        [BindProperty]
        public string search { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Subjects != null)
            {
                Subject = await _context.Subjects.ToListAsync();
            }
        }
        public async Task OnPostAsync()
        {
            if (search == null || search.Length == 0)
            {
                LoadSubjects();
            }
            else
            {
                SearchSubjectByName(search);
            }
        }
        void LoadSubjects()
        {
            Subject = _context.Subjects.ToList();
        }
        void SearchSubjectByName(string subjectName)
        {
            if (subjectName.Equals(""))
            {
                LoadSubjects();
            }
            else
            {
                Subject = _context.Subjects.Where(s => s.SubjectName.Contains(subjectName)).ToList();
            }
        }
    }
}
