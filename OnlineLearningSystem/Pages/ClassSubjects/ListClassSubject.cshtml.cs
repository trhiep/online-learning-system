using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.XWPF.UserModel;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class ListClassSubjectModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public ListClassSubjectModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IList<ClassSubject> ClassSubject { get; set; } = default!;


        //fake data for edit and check if form teacher is editting their class, you must get 2 these in onget 


        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormTeacherId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ClassId { get; set; }

        public string RoleName { get; set; }


        // For knowing if I need to show the button to create a new subject and manage Student in FE
        public bool IsAdminOrFormTeacher { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Check role and id user
            string accountIDString = HttpContext.Session.GetString("AccountIDSession");
            RoleName = HttpContext.Session.GetString("RoleSession");

            if (id == null || id == 0)
            {
                // Handle the case where id is not provided
                // If the user is a teacher, redirect them to the Classrooms List page
                if (RoleName.Equals(StaticString.StringRoleTeacher))
                {
                    return RedirectToPage("/Classrooms/List");
                }
                // If the user is a student, determine their class
                else if (RoleName.Equals(StaticString.StringRoleStudent))
                {
                    var temp = _context.ClassStudents
                        .Where(c => c.StudentId == int.Parse(accountIDString))
                        .Select(c => c.ClassId)
                        .FirstOrDefault();

                    if (temp != default(int))
                    {
                        ClassId = temp;
                    }
                    else
                    {
                        // Handle case where no class was found
                        // Optionally, redirect to an error page or show a message
                        // return RedirectToPage("/Error");
                    }
                }
                else if(RoleName.Equals(StaticString.StringRoleAdmin))
                {
                    
                }


                // If ClassId could not be determined, handle the case
                if (ClassId == 0)
                {
                    // Optionally, redirect to an error page or show a message
                    // return RedirectToPage("/Error");
                }
            }
            else
            {
                ClassId = (int)id;
            }

            if (int.TryParse(accountIDString, out int accountID))
            {
                if (_context.ClassSubjects != null)
                {
                    // Check if account access to this page is form teacher of this class or student of this class or admin
                    if (CommonMethod.IsFormTeacher(_context, accountID, ClassId)
                        || RoleName == StaticString.StringRoleAdmin
                        || CommonMethod.IsStudentOfClass(_context, accountID, ClassId))
                    {
                        // Get all subjects of this class by classId
                        ClassSubject = await _context.ClassSubjects
                            .Include(c => c.Class)
                            .Include(c => c.SubjectTeacherNavigation)
                            .Include(c => c.Subject)
                            .Where(c => c.Class.ClassId == ClassId)
                            .ToListAsync();
                        // Check if role of user is not student because student cannot create or manage student in their class
                        if (RoleName.Equals(StaticString.StringRoleTeacher) || RoleName.Equals(StaticString.StringRoleAdmin))
                        {
                            IsAdminOrFormTeacher = true;
                        }
                    }
                    else
                    {
                        // Get subjects that the teacher teaches in this class by classId
                        ClassSubject = await _context.ClassSubjects
                            .Include(c => c.Class)
                            .Include(c => c.SubjectTeacherNavigation)
                            .Include(c => c.Subject)
                            .Where(c => c.Class.ClassId == ClassId && c.SubjectTeacherNavigation.AccountId == accountID)
                            .ToListAsync();
                    }
                    // Get class name
                    var classroom = await _context.Classrooms
                        .Where(c => c.ClassId == ClassId)
                        .FirstOrDefaultAsync();

                    if (classroom != null)
                    {
                        ViewData["className"] = classroom.ClassName;
                        ViewData["formTeacherName"] = classroom.FormTeacher?.Fullname ?? "Not Assigned";
                    }
                }
            }
            else
            {
                return RedirectToPage("/Authen/Login");
            }

            return Page();
        }

    }
}
