using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record.Chart;
using NPOI.Util;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassStudents
{
    public class StudentListModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public StudentListModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? classId)
        {
            Console.WriteLine("OnGet: " + classId);
            HttpContext.Session.SetInt32("classId", (int)classId);
            return Page();
        }

        public IActionResult OnGetFetchStudent()
        {
            int classId = (int)HttpContext.Session.GetInt32("classId");
            Console.WriteLine("OnGetFetchStudent: " + classId);
            var data = _context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Where(c => c.ClassId == classId)
                .Select(student => new
                {
                    username = student.Student.Username,
                    fullname = student.Student.Fullname,
                    email = student.Student.Email,
                })
                .ToList();
            foreach ( var student in data )
            {
                Console.WriteLine(student.fullname);
            }

            return new JsonResult(new { data = data });
        }
    }
}
