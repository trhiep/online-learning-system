using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.Util;
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
        }
    }
}
