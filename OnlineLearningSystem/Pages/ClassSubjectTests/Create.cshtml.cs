using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils.Converter;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class CreateModel : PageModel
    {
        private OLS_DBContext _context;

        public CreateModel(OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassSubjectTest ClassSubjectTest { get; set; } = default!;

        [BindProperty]
        public int ClassSubjectId { get; set; } = default!;

        public IActionResult OnGet(int? classSubjectId)
        {
            if (classSubjectId != null)
            {
                ClassSubjectTest = new ClassSubjectTest();
                ClassSubjectTest.ClassSubjectId = (int)classSubjectId;
                return Page();
            } else
            {
                return RedirectToPage("/Index");
            }
            
        }

        [BindProperty]
        public string TestStartDate { get; set; } = default!;
        
        [BindProperty]
        public string TestEndDate { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            ClassSubjectTest.TestDescription = "";
            ClassSubjectTest.StartDate = DateTimeHelper.GetFormatedDateTimeFromString(TestStartDate);
            ClassSubjectTest.EndDate = DateTimeHelper.GetFormatedDateTimeFromString(TestEndDate);

            _context.ClassSubjectTests.Add(ClassSubjectTest);
            _context.SaveChanges();

            return RedirectToPage("/ClassSubjectTests/Index", new { classSubjectId = ClassSubjectTest.ClassSubjectId });
        }
    }
}
