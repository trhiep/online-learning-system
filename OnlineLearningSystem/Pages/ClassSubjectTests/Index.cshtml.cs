using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class IndexModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public Account LogedInAccount;
        public IndexModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClassSubject ThisClassSubject { get; set; } = default!;
        public IList<ClassSubjectTest> UnavailableClassSubjectTest { get; set; } = default!;
        public IList<ClassSubjectTest> NotStartedClassSubjectTest { get; set; } = default!;
        public IList<ClassSubjectTest> CompletedClassSubjectTest { get; set; } = default!;
        public IList<ClassSubjectTest> UncompletedClassSubjectTest { get; set; } = default!;
        public IList<ClassSubjectTest> CurrentClassSubjectTest { get; set; } = default!;
        public IActionResult OnGet(int? classSubjectId)
        {
            if (classSubjectId != null)
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
                {
                    string username = HttpContext.Session.GetString("UserSession");
                    LogedInAccount = _dbContext.Accounts.Where(x => x.Username == username).First();
                    if (CanUserAccessThisPage((int)classSubjectId))
                    {
                        ThisClassSubject = _dbContext.ClassSubjects
                            .Include(x => x.Class)
                            .Include(x => x.Subject)
                            .Where(x => x.ClassSubjectId == classSubjectId).First();

                        if (LogedInAccount.Role == "Teacher")
                        {
                            UnavailableClassSubjectTest = _dbContext.ClassSubjectTests
                                    .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                                  !_dbContext.TestQuestions
                                                           .Select(tq => tq.TestId)
                                                           .Contains(cst.TestId)).ToList();

                            NotStartedClassSubjectTest = _dbContext.ClassSubjectTests
                                    .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                    _dbContext.TestQuestions.Select(tq => tq.TestId).Contains(cst.TestId) &&
                                                  cst.StartDate > DateTime.Now).ToList();
                        }

                        CompletedClassSubjectTest = _dbContext.ClassSubjectTests
                                    .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                                  cst.EndDate < DateTime.Now).ToList();

                        CurrentClassSubjectTest = _dbContext.ClassSubjectTests
                                    .Where(cst => cst.ClassSubjectId == classSubjectId &&
                                    _dbContext.TestQuestions.Select(tq => tq.TestId).Contains(cst.TestId) &&
                                    DateTime.Now >= cst.StartDate && DateTime.Now <= cst.EndDate).ToList();
                        return Page();
                    }
                }
            }
            return RedirectToPage("/Index");
        }

        public bool CanUserAccessThisPage(int classSubjectId)
        {
            var thisClassSubject = _dbContext.ClassSubjects
                .Where(x => x.ClassSubjectId == classSubjectId)
                .First();
            if (thisClassSubject != null && LogedInAccount != null)
            {
                if (LogedInAccount.Role == "Teacher")
                {
                    if (thisClassSubject.SubjectTeacher == LogedInAccount.AccountId) return true;
                }

                if (LogedInAccount.Role == "Student")
                {
                    var classStudent = _dbContext.ClassStudents
                        .Where(x => x.StudentId == LogedInAccount.AccountId && x.ClassId == thisClassSubject.ClassId).First();
                    if (thisClassSubject != null) return true;
                }
            }
            return false;
        }
    }
}
