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
    public class CreateModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public CreateModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClassId"] = new SelectList(_context.Classrooms, "ClassId", "ClassName");
            ViewData["SubjectList"] = _context.Subjects.ToList();
            ViewData["TeacherList"] = _context.Accounts.Where(a => a.Role.Equals(StaticString.StringRoleTeacher)).ToList();

            return Page();
        }

        [BindProperty]
        public ClassSubject ClassSubject { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsSubjectAlreadyInClass(ClassSubject.ClassId, ClassSubject.SubjectId))
            {
                _context.ClassSubjects.Add(ClassSubject);
                await _context.SaveChangesAsync();
                return RedirectToPage("./ListClassSubject");
            }
            else
            {
                var className = _context.Classrooms.FirstOrDefault(c => c.ClassId == ClassSubject.ClassId)?.ClassName;
                ViewData["Error"] = StaticString.THIS_SUBJECT_EXIST + " in " + className;
                PopulateViewData();
                return Page();
            }
        }

        private void PopulateViewData()
        {
            ViewData["ClassId"] = new SelectList(_context.Classrooms, "ClassId", "ClassName");
            ViewData["SubjectList"] = _context.Subjects.ToList();
            ViewData["TeacherList"] = _context.Accounts.Where(a => a.Role.Equals(StaticString.StringRoleTeacher)).ToList();
        }

        public bool IsSubjectAlreadyInClass(int classId, int subjectId)
        {
            return _context.ClassSubjects.Any(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        }
    }
}
