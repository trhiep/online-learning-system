using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // Property to bind the ClassSubject model
        [BindProperty]
        public ClassSubject ClassSubject { get; set; } = default!;

        // Property to store ClassID
        [BindProperty]
        public int ClassID { get; set; }

        // Property to store Classroom information
        public Classroom Class { get; set; }

        // GET method to load the form
        public IActionResult OnGet(int? id)
        {
            // Check if id is null
            if (id == null)
            {
                return NotFound();
            }

            // Assign the ClassID
            ClassID = (int)id;

            // Get accountID and role from session
            string accountIDString = HttpContext.Session.GetString("AccountIDSession");

            // If accountID is valid, populate the view data
            if (int.TryParse(accountIDString, out int accountID))
            {
                PopulateViewData(accountID);
            }
            return Page();
        }

        // POST method to handle form submission
        public async Task<IActionResult> OnPostAsync()
        {
            string accountIDString = HttpContext.Session.GetString("AccountIDSession");

            if (int.TryParse(accountIDString, out int accountID))
            {
                PopulateViewData(accountID);
            }
            


            // Check if the subject is already in the class
            if (!IsSubjectAlreadyInClass(ClassSubject.ClassId, ClassSubject.SubjectId))
            {
                // Add the new ClassSubject to the database
                _context.ClassSubjects.Add(ClassSubject);
                await _context.SaveChangesAsync();
                // Redirect to the ListClassSubject page with ClassID
                return RedirectToPage("./ListClassSubject", new { id = ClassID });
            }
            else
            {
                // Show error if subject already exists in the class
                var className = _context.Classrooms.FirstOrDefault(c => c.ClassId == ClassSubject.ClassId)?.ClassName;
                ViewData["ToastMessage"] = StaticString.THIS_SUBJECT_EXIST + " in " + className;
                return Page();
            }
        }

        // Method to populate view data with necessary information
        private void PopulateViewData(int accountID)
        {
            // Get the classroom information
            var classroom = _context.Classrooms.FirstOrDefault(x => x.ClassId == ClassID);
            if (classroom != null)
            {
                Class = classroom;
                ViewData["ClassName"] = classroom.ClassName; // Set ClassName in ViewData
            }

            // Get the list of subjects and teachers
            ViewData["SubjectList"] = _context.Subjects.ToList();
            ViewData["TeacherList"] = _context.Accounts
                .Where(a => a.Role.Equals(StaticString.StringRoleTeacher))
                .ToList();
        }


        // Method to check if the subject is already in the class
        public bool IsSubjectAlreadyInClass(int classId, int subjectId)
        {
            return _context.ClassSubjects.Any(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        }
    }
}
