using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils;
using OnlineLearningSystem.Utils.Converter;
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
        }

        public ClassSubjectTest TestInformation { get; set; } = default!;

        public List<Question> Questions { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int? testId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                string username = HttpContext.Session.GetString("UserSession");
                LogedInAccount = _dbContext.Accounts.Where(x => x.Username == username).First();
                if (testId != null && LogedInAccount != null && LogedInAccount.Role == "Student")
                {
                    TestInformation = await _dbContext.ClassSubjectTests.Where(x => x.TestId == testId).FirstAsync();

                    var oldAnswerAttempt = _dbContext.StudentTestAnswers
                        .Where(x => x.TestId == testId && x.StudentId == LogedInAccount.AccountId)
                        .OrderByDescending(x => x.AttemptNo).FirstOrDefault();
                    if (oldAnswerAttempt != null)
                    {
                        var newAttemptNo = oldAnswerAttempt.AttemptNo + 1;
                        AttemptNo = newAttemptNo;
                    }
                    else
                    {
                        AttemptNo = 1;
                    }

                    if (AttemptNo > TestInformation.Attempts)
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
            }
            
            return RedirectToPage("/Authen/Login");
        }

        [BindProperty]
        public string? AnswersJSON { get; set; }

        [BindProperty]
        public int ThisTestId { get; set; } = default!;
        
        [BindProperty]
        public int AttemptNo { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(AnswersJSON);
            SaveStudentAnswersToDB(AnswersJSON, ThisTestId, AttemptNo);
            return RedirectToPage("/ClassSubjectTests/TestDetails", new { testId = ThisTestId });
        }

        public JsonResult OnPostSaveData(string jsonData, string testId, string attemptNo)
        {
            Console.WriteLine(jsonData);
            SaveStudentAnswersToDB(jsonData, int.Parse(testId), int.Parse(attemptNo));
            return new JsonResult(new { success = true });
        }

        public JsonResult OnPostSaveLog(string message, int testId)
        {
            string username = HttpContext.Session.GetString("UserSession");
            var thisTest = _dbContext.ClassSubjectTests.Where(x => x.TestId == testId).First();

            string buildDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = Path.Combine(buildDirectory, "Logs/" + TextConvert.RemoveDiacritics(thisTest.TestName) + "/" + username);
            string logFileName = username + ".txt";

            Logger logger = new Logger(logDirectory, logFileName);

            logger.Log(username, message);

            return new JsonResult(new { success = true });
        }

        private void SaveStudentAnswersToDB(string dataJSON, int testId, int attemptNo)
        {
            List<StudentTestAnswer> studentTestAnswers = GetStudentTestAnswerFromJSON(dataJSON, testId);
            if (studentTestAnswers.Any())
            {
                var oldAnswers = _dbContext.StudentTestAnswers.Where(x => x.TestId == testId && x.AttemptNo == attemptNo && x.StudentId == studentTestAnswers[0].StudentId).ToList();
                if (oldAnswers.Any())
                {
                    foreach (var oldAnswer in oldAnswers)
                    {
                        _dbContext.StudentTestAnswers.Remove(oldAnswer);
                        _dbContext.SaveChanges();
                    }
                }

                foreach (var testAnswers in studentTestAnswers)
                {
                    testAnswers.AttemptNo = attemptNo;
                    testAnswers.AnswerTime = DateTime.Now;
                    _dbContext.StudentTestAnswers.Add(testAnswers);
                    _dbContext.SaveChanges();
                }
            }
        }

        private List<StudentTestAnswer> GetStudentTestAnswerFromJSON(string jsonData, int testId)
        {
            List<StudentTestAnswer> studentTestAnswers = new List<StudentTestAnswer>();
            string username = HttpContext.Session.GetString("UserSession");
            var thisAccount = _dbContext.Accounts.Where(x => x.Username == username).First();
            var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, List<AnswerDTO>>>(jsonData);
            foreach (KeyValuePair<string, List<AnswerDTO>> entry in deserializedData)
            {
                foreach (var item in entry.Value)
                {
                    if (item.IsChecked == true)
                    {
                        var newStudentTestAnswer = new StudentTestAnswer();
                        newStudentTestAnswer.StudentId = thisAccount.AccountId;
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
        public string AttemptNo { get; set; }
        public string? AnswersData { get;}
    }
}
