using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Pages.Dashboard;

namespace OnlineLearningSystem.Pages.UserProfile
{
    public class StudentProfileModel : PageModel
    {
        public IActionResult OnGet()
        {
            Console.WriteLine(string.IsNullOrEmpty(HttpContext.Session.GetString("RoleSession")) && HttpContext.Session.GetString("RoleSession") == "Teacher");
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("RoleSession")) && HttpContext.Session.GetString("RoleSession") == "Teacher")
            {
                TempData["ErrorRole"] = "Login with admin to access";
                return RedirectToPage("/Error");
            }

            // Người dùng đã đăng nhập, tiếp tục xử lý
            return Page();
        }
        private readonly OLS_DBContext _context = new OLS_DBContext();


        [BindProperty]
        public ClassViewModel ClassViewModel { get; set; }

        //public async Task<IActionResult> OnGetAsync(int id)
        //{
        //    ClassViewModel = await (from account in _context.Accounts
        //                            join classStudent in _context.ClassStudents on account.AccountId equals classStudent.StudentId
        //                            join classroom in _context.Classrooms on classStudent.ClassId equals classroom.ClassId
        //                            where classStudent.ClassId == id
        //                            select new ClassViewModel
        //                            {
        //                                Account = account,
        //                                ClassStudent = classStudent,
        //                                Classroom = classroom
        //                            }).FirstOrDefaultAsync();

        //    if (ClassViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var accountToUpdate = await _context.Accounts.FindAsync(ClassViewModel.Account.AccountId);
            var classStudentToUpdate = await _context.ClassStudents.FindAsync(ClassViewModel.ClassStudent.StudentId, ClassViewModel.ClassStudent.ClassId);
            var classroomToUpdate = await _context.Classrooms.FindAsync(ClassViewModel.Classroom.ClassId);

            if (accountToUpdate == null || classStudentToUpdate == null || classroomToUpdate == null)
            {
                return NotFound();
            }

            // Update the properties
            accountToUpdate.Fullname = ClassViewModel.Account.Fullname;
            accountToUpdate.Email = ClassViewModel.Account.Email;

            classStudentToUpdate.ClassId = ClassViewModel.ClassStudent.ClassId;

            classroomToUpdate.ClassName = ClassViewModel.Classroom.ClassName;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
