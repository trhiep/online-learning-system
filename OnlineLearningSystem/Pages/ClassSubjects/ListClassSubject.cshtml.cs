using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public int ClassId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormTeacherId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ClassId { get; set; }

        public string RoleName { get; set; }


        // For knowing if I need to show the button to create a new subject and manage Student in FE
        public bool IsAdminOrFormTeacher { get; set; } = false;


        public async Task OnGetAsync(int? id, int? formTeacherId)
        {
            ClassId = (int)id;
            FormTeacherId = (int)formTeacherId;
            var username = HttpContext.Session.GetString("UserSession");
            var Account = _context.Accounts.FirstOrDefault(a => a.Username == username);


            ClassSubject = await _context.ClassSubjects
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.SubjectTeacherNavigation)
                .Where(c => c.Class.ClassId == ClassId)
                .ToListAsync();

            //if (IsFormTeacher(AccountId, ClassId))
            //{

            //}

            var classroom = await _context.Classrooms.Where(c => c.ClassId == ClassId).FirstOrDefaultAsync();
            var account = await _context.Accounts.Where(a => a.AccountId == FormTeacherId).FirstOrDefaultAsync();
            ViewData["className"] = classroom.ClassName;
            ViewData["formTeacherName"] = account.Fullname;
        }

        private bool IsFormTeacher(int teacherID, int classID)
        {
            var classRoom = _context.Classrooms.Where(c => c.ClassId == classID && c.FormTeacherId == teacherID);
            if (classRoom != null) return true;
            return false;

        public async Task OnGetAsync(int? id)
        {
            ClassId = (int)id;
            // Check role and id user
            string accountIDString = HttpContext.Session.GetString("AccountIDSession");
            RoleName = HttpContext.Session.GetString("RoleSession");

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
                    }
                }
            }
            else
            {
                // Handle invalid AccountID session value
                // For example, redirect to login or show a message
                // Example:
                // return RedirectToPage("/Login");
            }

        }
    }
}
