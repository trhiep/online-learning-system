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
        
        public IndexModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;

            LogedInAccount = new Account();
            LogedInAccount = _dbContext.Accounts.Where(x => x.AccountId == 7).First();
        }

        public IList<ClassSubjectTest> InactiveClassSubjectTest { get; set; } = default!;

        public IList<ClassSubjectTest> ActiveClassSubjectTest { get; set; } = default!;

        public IActionResult OnGet(int? classSubjectId)
        {
            if (classSubjectId != null)
            {
                var classSubjectInformation = _dbContext.ClassSubjects
                    .Include(x => x.Class)
                    .Include(x => x.Subject)
                    .Where(x => x.ClassSubjectId == classSubjectId).First();

                if (classSubjectInformation != null)
                {
                    if (LogedInAccount != null && LogedInAccount.Role == "Teacher" && classSubjectInformation.SubjectTeacher == LogedInAccount.AccountId)
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
                            .Where(x => x.ClassId == classSubjectInformation.ClassId && x.StudentId == LogedInAccount.AccountId)
                            .FirstOrDefault();
                        if (thisClassStudent == null) return RedirectToPage("/Index");
                    }

                    ActiveClassSubjectTest = _dbContext.ClassSubjectTests
                        .Where(cst => cst.ClassSubjectId == classSubjectId &&
                              _dbContext.TestQuestions
                                       .Select(tq => tq.TestId)
                                       .Contains(cst.TestId) &&
                              DateTime.Now >= cst.StartDate && DateTime.Now <= cst.EndDate).ToList();
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
