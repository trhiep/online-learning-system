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

        //fake data for edit and check if form teacher is edit their class, you must get 2 these in onget 
        [BindProperty(SupportsGet = true)]
        public int ClassId { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int AccountId { get; set; } = 1;


        public async Task OnGetAsync()
        {
            //check role user
            if (true)
            {
                //check if data send to this not null
                if (_context.ClassSubjects != null && ClassId != null && AccountId != null)
                {
                    //check if account access to this page is form teacher of this class
                    if (IsFormTeacher(AccountId, ClassId))
                    {
                        ClassSubject = await _context.ClassSubjects
                    .Include(c => c.Class)
                    .Include(c => c.Subject)
                    .Include(c => c.SubjectTeacherNavigation)
                    .Where(c => c.Class.ClassId == ClassId)
                    .ToListAsync();

                        var classroom = await _context.Classrooms.Where(c => c.ClassId == ClassId).FirstOrDefaultAsync();
                        ViewData["className"] = classroom.ClassName;
                    }
                }
            }
        }

        private bool IsFormTeacher(int teacherID, int classID)
        {
            var classRoom = _context.Classrooms.Where(c => c.ClassId == classID && c.FormTeacherId == teacherID);
            if (classRoom != null) return true;
            return false;
        }
    }
}
