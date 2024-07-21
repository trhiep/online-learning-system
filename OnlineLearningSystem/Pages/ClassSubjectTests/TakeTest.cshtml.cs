using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils.Extensions;
using System.Collections.Generic;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class TakeTestModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public Account LogedInAccount;
        public TakeTestModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
            LogedInAccount = new Account();
            LogedInAccount = _dbContext.Accounts.Where(x => x.AccountId == 6).First();
        }

        public ClassSubjectTest TestInformation { get; set; } = default!;

        public List<Question> Questions { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if(testId != null)
            {
                TestInformation = await _dbContext.ClassSubjectTests.Where(x => x.TestId == testId).FirstAsync();

                var oldAnswerAttempt = _dbContext.StudentTestAnswers
                    .Where(x=> x.TestId == testId && x.StudentId == LogedInAccount.AccountId)
                    .OrderByDescending(x => x.AttemptNo).FirstOrDefault();
                if (oldAnswerAttempt != null)
                {
                    var newAttemptNo = oldAnswerAttempt.AttemptNo + 1;
                    AttemptNo = newAttemptNo;
                } else {
                    AttemptNo = 1; 
                }

                if(AttemptNo > TestInformation.Attempts)
                {
                    TempData["ToastMessage"] = "Bạn đã hết lượt làm bài thi này!";
                    return RedirectToPage("/ClassSubjectTests/TestDetails", new { testId = TestInformation.TestId });
                }

                var questions = await _dbContext.TestQuestions.Include(x => x.Answers).Where(x => x.TestId == testId).ToListAsync();
                Questions = new List<Question>();
                foreach (var question in questions)
                {
                    Question newQuestion = new Question()
                    {
                        QuestionId = question.TestQuestionId,
                        QuestionText = question.Content
                    };

                    int count = 0;
                    foreach (var ans in question.Answers)
                    {
                        newQuestion.Answers.Add(new AnswerDTO()
                        {
                            QuestionId = question.TestQuestionId,
                            AnswerId = ans.QuestionAnswerId.ToString(),
                            Content = ans.Content
                        });

                        if (ans.IsCorrectAnswer) count++;
                    }
                    newQuestion.Description = "Chọn " + count + " đáp án";

                    Questions.Add(newQuestion);
                }
                
                return Page();
            }
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public string? AnswersJSON { get; set; }

        [BindProperty]
        public int ThisTestId { get; set; } = default!;
        
        [BindProperty]
        public int AttemptNo { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            List<StudentTestAnswer> studentTestAnswers = GetStudentTestAnswerFromJSON(AnswersJSON, ThisTestId);
            if (studentTestAnswers.Any())
            {
                var oldAnswers = await _dbContext.StudentTestAnswers.Where(x => x.TestId == ThisTestId && x.AttemptNo == AttemptNo).ToListAsync();
                if (oldAnswers.Any())
                {
                    foreach (var oldAnswer in oldAnswers)
                    {
                        _dbContext.StudentTestAnswers.Remove(oldAnswer);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                foreach (var testAnswers in studentTestAnswers)
                {
                    testAnswers.AttemptNo = AttemptNo;
                    testAnswers.AnswerTime = DateTime.Now;
                    _dbContext.StudentTestAnswers.Add(testAnswers);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return RedirectToPage("/ClassSubjectTests/TestDetails", new { testId = ThisTestId });
        }

        public JsonResult OnPostSaveData()
        {
         
            return new JsonResult(new { success = true });
        }

        private List<StudentTestAnswer> GetStudentTestAnswerFromJSON(string jsonData, int testId)
        {
            List<StudentTestAnswer> studentTestAnswers = new List<StudentTestAnswer>();

            var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, List<AnswerDTO>>>(jsonData);
            foreach (KeyValuePair<string, List<AnswerDTO>> entry in deserializedData)
            {
                foreach (var item in entry.Value)
                {
                    if (item.IsChecked == true)
                    {
                        var newStudentTestAnswer = new StudentTestAnswer();
                        newStudentTestAnswer.StudentId = LogedInAccount.AccountId;
                        newStudentTestAnswer.TestId = testId;
                        newStudentTestAnswer.TestQuestionId = int.Parse(entry.Key);
                        newStudentTestAnswer.SelectedAnswerId = int.Parse(item.AnswerId);
                        studentTestAnswers.Add(newStudentTestAnswer);
                    }
                }
            }
            return studentTestAnswers;
        }
    }

    public class TestAnswerFetchData
    {
        public string TestId { get; set; }
        public string? AnswersData { get;}
    }
}
