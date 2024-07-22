using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;
using SixLabors.ImageSharp.ColorSpaces;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public class IndexModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public Account LogedInAccount;
        //Hieu
        public List<ClassSubjectPost> Posts { get; set; }

        public IndexModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<ClassSubjectTest> InactiveClassSubjectTest { get; set; } = default!;

        public IList<ClassSubjectTest> ActiveClassSubjectTest { get; set; } = default!;

        public ClassSubject ThisClassSubject { get; set; } = default!;

        public IActionResult OnGet(int? classSubjectId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                string username = HttpContext.Session.GetString("UserSession");
                LogedInAccount = _dbContext.Accounts.Where(x => x.Username == username).First();

                if (classSubjectId != null)
                {
                    ThisClassSubject = _dbContext.ClassSubjects
                        .Include(x => x.Class)
                        .Include(x => x.Subject)
                        .Where(x => x.ClassSubjectId == classSubjectId).First();

                    if (ThisClassSubject != null)
                    {
                        if (LogedInAccount != null && LogedInAccount.Role == "Teacher" && ThisClassSubject.SubjectTeacher == LogedInAccount.AccountId)
                        {
                            InactiveClassSubjectTest = _dbContext.ClassSubjectTests
                                    .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                                  !_dbContext.TestQuestions
                                                           .Select(tq => tq.TestId)
                                                           .Contains(cst.TestId)).ToList();
                        }

                        if (LogedInAccount != null && LogedInAccount.Role == "Student")
                        {
                            var thisClassStudent = _dbContext.ClassStudents
                                .Where(x => x.ClassId == ThisClassSubject.ClassId && x.StudentId == LogedInAccount.AccountId)
                                .FirstOrDefault();
                            if (thisClassStudent == null) return RedirectToPage("/Index");
                        }

                        ActiveClassSubjectTest = _dbContext.ClassSubjectTests
                            .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                  _dbContext.TestQuestions
                                           .Select(tq => tq.TestId)
                                           .Contains(cst.TestId) &&
                                  DateTime.Now >= cst.StartDate && DateTime.Now <= cst.EndDate).ToList();
                        
                        //load list ClassSubjectPost
                        
                        return Page();
                    }

                }
                return RedirectToPage("/Index");
            } else
            {
                return RedirectToPage("/Authen/Login");
            }
        }
    }
}
