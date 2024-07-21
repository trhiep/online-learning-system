using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class TestDetailsModel : PageModel
    {
        OLS_DBContext _dbContext;
        public Account LogedInAccount;

        public TestDetailsModel(OLS_DBContext context)
        {
            _dbContext = context;
            LogedInAccount = new Account();
            LogedInAccount = _dbContext.Accounts.Where(x => x.AccountId == 6).First();
        }
        [BindProperty]
        public ClassSubjectTest ClassSubjectTest { get; set; } = default!;

        public List<TestResultDTO> TestResults { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            
            if(testId != null)
            {
                ClassSubjectTest = await _dbContext.ClassSubjectTests.Include(x => x.TestQuestions).Where(x => x.TestId == testId).FirstOrDefaultAsync();
                if (ClassSubjectTest != null)
                {
                    TestResults = new List<TestResultDTO>();
                    TestResults = GetTestResults(LogedInAccount.AccountId, ClassSubjectTest.TestId);
                } else
                {
                    return RedirectToPage("/Index");
                }
            }

            if (TempData.ContainsKey("ToastMessage"))
            {
                ViewData["ToastMessage"] = TempData["ToastMessage"];
            }
            return Page();
        }


        private List<TestResultDTO> GetTestResults(int studentId, int testId)
        {
            // Lấy tổng số câu hỏi cho bài kiểm tra
            var totalQuestions = _dbContext.TestQuestions
                .Count(tq => tq.TestId == testId);

            // Lấy tất cả các câu trả lời đúng cho mỗi câu hỏi
            var correctAnswers = _dbContext.Answers
                .Where(a => a.IsCorrectAnswer)
                .AsEnumerable() // Chuyển sang client-side để sử dụng GroupBy
                .GroupBy(a => a.QuestionId)
                .ToDictionary(g => g.Key, g => g.Select(a => a.QuestionAnswerId).ToList());

            // Lấy tất cả các câu trả lời của sinh viên
            var studentAnswers = _dbContext.StudentTestAnswers
                .Where(sta => sta.TestId == testId && sta.StudentId == studentId)
                .ToList();

            // Nhóm câu trả lời của sinh viên theo số lần thử
            var groupedByAttempts = studentAnswers
                .GroupBy(sta => sta.AttemptNo)
                .ToList();

            // Danh sách kết quả
            var testResults = new List<TestResultDTO>();

            // Tính toán điểm số cho mỗi lần thử
            foreach (var attemptGroup in groupedByAttempts)
            {
                var attemptNo = attemptGroup.Key;
                var takenDate = attemptGroup.Max(sta => sta.AnswerTime);

                // Nhóm câu trả lời của sinh viên theo TestQuestionId
                var studentAnswersGrouped = attemptGroup
                    .GroupBy(sta => sta.TestQuestionId)
                    .ToDictionary(g => g.Key, g => g.Select(sta => sta.SelectedAnswerId).ToList());

                // Tính số câu trả lời đúng của sinh viên
                var correctCount = studentAnswersGrouped
                    .Sum(g =>
                    {
                        var questionId = g.Key;
                        var correctAnswerIds = correctAnswers.ContainsKey(questionId) ? correctAnswers[questionId] : new List<int>();
                        var studentAnswerIds = g.Value;

                        // Tính số câu trả lời đúng
                        return correctAnswerIds.Intersect(studentAnswerIds).Count() > 0 ? 1 : 0;
                    });

                // Tính điểm
                var score = correctCount * (10.0 / totalQuestions);

                // Xác định trạng thái (Ví dụ: "Passed" nếu điểm >= 5, ngược lại "Failed")
                var status = score >= 5 ? "ĐẠT" : "KHÔNG ĐẠT";

                // Map vào class TestResult
                var testResult = new TestResultDTO
                {
                    AttemptsNo = attemptNo,
                    TakenDate = takenDate,
                    CorrectAnswers = correctCount,
                    Grade = (float)score,
                    Status = status
                };

                // Thêm vào danh sách kết quả
                testResults.Add(testResult);
            }

            return testResults;
        }


    }

    public class TestResultDTO
    {
        public int AttemptsNo { get; set; }
        public DateTime TakenDate { get; set; }
        public int CorrectAnswers { get; set; }
        public float Grade { get; set; }
        public string Status { get; set; }
    }
}
