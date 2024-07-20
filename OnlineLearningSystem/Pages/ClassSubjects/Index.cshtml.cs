using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;

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
            LogedInAccount = _dbContext.Accounts.Where(x => x.AccountId == 6).First();
        }

        public IList<ClassSubjectTest> InactiveClassSubjectTest { get; set; } = default!;

        public IList<ClassSubjectTest> ActiveClassSubjectTest { get; set; } = default!;

        public void OnGet()
        {
            if (LogedInAccount != null && LogedInAccount.Role == "Teacher")
            {
                InactiveClassSubjectTest = _dbContext.ClassSubjectTests
                        .Where(cst => cst.ClassSubjectId == 1 && //Replace class subject id here
                                      !_dbContext.TestQuestions
                                               .Select(tq => tq.TestId)
                                               .Contains(cst.TestId)).ToList();
            }

            ActiveClassSubjectTest = _dbContext.ClassSubjectTests
                        .Where(cst => cst.ClassSubjectId == 1 && //Replace class subject id here
                                      _dbContext.TestQuestions
                                               .Select(tq => tq.TestId)
                                               .Contains(cst.TestId)).ToList();

        }
    }
}
