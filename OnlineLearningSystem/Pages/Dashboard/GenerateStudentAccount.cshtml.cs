using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;
using System.Text.Json;

namespace OnlineLearningSystem.Pages.Dashboard
{
	public class ClassViewModel
	{
		public Account Account { get; set; }
		public ClassStudent ClassStudent { get; set; }
		public Classroom Classroom { get; set; }
	}


	public class GenerateStudentAccountModel : PageModel
    {

		private readonly OLS_DBContext _context = new OLS_DBContext();



		public IList<ClassViewModel> ClassList { get; set; }
		public List<Classroom> ClassroomList { get; set; }
		public IActionResult OnGet()
		{
   

            ClassroomList = _context.Classrooms.ToList();
			ClassList = (from account in _context.Accounts
						 join classStudent in _context.ClassStudents on account.AccountId equals classStudent.StudentId
						 join classroom in _context.Classrooms on classStudent.ClassId equals classroom.ClassId
						 where classStudent.ClassId == 1
						 select new ClassViewModel
						 {
							 Account = account,
							 ClassStudent = classStudent,
							 Classroom = classroom
						 }).ToList();

			return Page();
		}
        public IActionResult OnGetStudents(int? classId)
        {
           
            var students = (from account in _context.Accounts
                            join classStudent in _context.ClassStudents on account.AccountId equals classStudent.StudentId
                            join classroom in _context.Classrooms on classStudent.ClassId equals classroom.ClassId
                            where classStudent.ClassId == classId
                            select new
                            {
                                Account = account,
                                ClassStudent = classStudent,
                                Classroom = classroom
                              
                            }).ToList();
            Console.WriteLine("/--- Students: " + JsonSerializer.Serialize(students));
            return new JsonResult(students);
        }
    }
}
