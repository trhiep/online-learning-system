using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils.Converter;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class AddQuestionModel : PageModel
    {
        private OLS_DBContext _dbContext;
        public Account LogedInAccount;

        public AddQuestionModel(OLS_DBContext dbContext)
        {
            _dbContext = dbContext;
            LogedInAccount = new Account();
            LogedInAccount = _dbContext.Accounts.Where(x => x.AccountId == 6).First();
        }

        [BindProperty]
        public IList<TestQuestion> TestQuestions { get; set; } = default!;
        public ClassSubjectTest SubjectTest { get; set; } = default!;
        public IActionResult OnGet(int? testId)
        {
            if (testId == null)
            {
                return RedirectToPage("/ClassSubjects/Index");
            }
            else
            {
                var classSubject = _dbContext.ClassSubjectTests
                    .Where(t => t.TestId == testId)
                    .Select(t => t.ClassSubject)
                    .FirstOrDefault();

                if (classSubject == null || classSubject.SubjectTeacher != LogedInAccount.AccountId)
                {
                    return RedirectToPage("/ClassSubjects/Index");
                }
            }
            SubjectTest = _dbContext.ClassSubjectTests.Where(t => t.TestId == testId).First();
            TestQuestions = _dbContext.TestQuestions.Where(x => x.TestId == testId).OrderByDescending(x => x.TestQuestionId).ToList();
            List<Answer> Answers = new List<Answer>();
            if (TestQuestions.Any())
            {
                foreach (var item in TestQuestions)
                {
                    List<Answer> answersOfAQuestion = _dbContext.Answers
                        .Where(x => x.QuestionId == item.TestQuestionId)
                        .ToList();
                    if (answersOfAQuestion.Count > 0)
                    {
                        Answers.AddRange(answersOfAQuestion);
                    }
                }
                ViewData["Answers"] = Answers;
            }

            ViewData["testId"] = testId;
            if (TempData.ContainsKey("ToastMessage"))
            {
                ViewData["ToastMessage"] = TempData["ToastMessage"];
            }
            return Page();
        }

        [BindProperty]
        public TestQuestion TestQuestion { get; set; } = default!;
        [BindProperty]
        public string? AnswersJSON { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            TestQuestion.ImageLink = "";
            TestQuestion.LastModifiedDate = DateTime.Now;

            _dbContext.TestQuestions.Add(TestQuestion);
            await _dbContext.SaveChangesAsync();

            var answers = JsonConvert.DeserializeObject<List<AnswerDTO>>(AnswersJSON);
            foreach (var item in answers)
            {
                var newAnswer = new Answer()
                {
                    QuestionId = TestQuestion.TestQuestionId,
                    Content = item.Content,
                    ImageLink = "",
                    LastModifiedDate = DateTime.Now,
                    IsCorrectAnswer = item.IsChecked
                };
                _dbContext.Answers.Add(newAnswer);
                await _dbContext.SaveChangesAsync();
                //Console.WriteLine("Answer: " + item.Content + " ----- IsCorrect: " + item.IsChecked);
            }
            var currentTestId = TestQuestion.TestId;
            TestQuestion = new TestQuestion();
            AnswersJSON = null;
            TempData["ToastMessage"] = "Thêm câu hỏi mới thành công";
            return RedirectToPage("/ClassSubjectTests/AddQuestion", new { testId = currentTestId });
        }

        [BindProperty]
        public string EditAnswersJSON { get; set; } = default!;
        [BindProperty]
        public string EditQuestionId { get; set; }
        public async Task<IActionResult> OnPostEditQuestion()
        {
            try
            {
                int edittingQuestionId = int.Parse(EditQuestionId);
                var thisTestQuestion = _dbContext.TestQuestions.Where(x => x.TestQuestionId == edittingQuestionId).First();
                if (thisTestQuestion != null)
                {
                    var newAnswers = JsonConvert.DeserializeObject<List<AnswerDTO>>(EditAnswersJSON);
                    foreach (var item in newAnswers)
                    {
                        if (int.Parse(item.AnswerId) == 0)
                        {
                            Answer newAnswer = new Answer()
                            {
                                QuestionId = thisTestQuestion.TestQuestionId,
                                Content = item.Content,
                                ImageLink = "",
                                LastModifiedDate = DateTime.Now,
                                IsCorrectAnswer = item.IsChecked
                            };
                            _dbContext.Answers.Add(newAnswer);
                            await _dbContext.SaveChangesAsync();

                        }
                        else
                        {
                            var oldAnswer = await _dbContext.Answers.Where(x => x.QuestionAnswerId == int.Parse(item.AnswerId)).FirstAsync();
                            oldAnswer.Content = item.Content;
                            oldAnswer.LastModifiedDate = DateTime.Now;
                            oldAnswer.IsCorrectAnswer = item.IsChecked;
                            _dbContext.Answers.Update(oldAnswer);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    TempData["ToastMessage"] = "Cập nhật câu hỏi thành công";
                    return RedirectToPage("/ClassSubjectTests/AddQuestion", new { testId = thisTestQuestion.TestId });
                }
            }
            catch (Exception ex)
            {

            }
            TempData["ToastMessage"] = "Có lỗi xảy ra khi cập nhật câu trả lời";
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public IFormFile ExcelFile { get; set; } = default!;
        [BindProperty]
        public string TestID { get; set; } = default!;
        public async Task<IActionResult> OnPostImportQuestion()
        {
            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                TempData["ToastMessage"] = "File Excel không được để trống!";
                return RedirectToPage("/ClassSubjectTests/AddQuestion", new { testId = int.Parse(TestID) });
            }

            // Read file excel here
            var questions = await ReadExcelAsync(ExcelFile);
            foreach (var item in questions)
            {
                var newQuestion = new TestQuestion()
                {
                    TestId = int.Parse(TestID),
                    Content = item.QuestionText,
                    ImageLink = "",
                    LastModifiedDate = DateTime.Now
                };
                _dbContext.TestQuestions.Add(newQuestion);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine("==============");
                Console.WriteLine("Question: " + item.QuestionText);
                Console.WriteLine("Answer: ");

                foreach (var ans in item.Answers)
                {
                    Console.WriteLine("Ans: " + ans.Content + " ---- " + "IsCorrect: " + ans.IsChecked);
                    var newAnswer = new Answer()
                    {
                        QuestionId = newQuestion.TestQuestionId,
                        Content = ans.Content,
                        ImageLink = "",
                        LastModifiedDate = DateTime.Now,
                        IsCorrectAnswer = ans.IsChecked
                    };
                    _dbContext.Answers.Add(newAnswer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            TempData["ToastMessage"] = "Nhập câu hỏi từ file Excel thành công!";
            return RedirectToPage("/ClassSubjectTests/AddQuestion", new { testId = int.Parse(TestID) });
        }

        private async Task<List<Question>> ReadExcelAsync(IFormFile excelFile)
        {
            var questions = new List<Question>();
            Question currentQuestion = null;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    IRow currentRow = sheet.GetRow(row);
                    if (currentRow != null)
                    {
                        ICell questionCell = currentRow.GetCell(0);
                        ICell answerContentCell = currentRow.GetCell(1);
                        ICell isCheckedCell = currentRow.GetCell(2);

                        // Check if the first cell contains a new question
                        if (questionCell != null && !string.IsNullOrWhiteSpace(GetCellValueAsString(questionCell)))
                        {
                            currentQuestion = new Question
                            {
                                QuestionText = GetCellValueAsString(questionCell)
                            };
                            questions.Add(currentQuestion);
                        }

                        // Add answers to the current question
                        if (answerContentCell != null && isCheckedCell != null && currentQuestion != null)
                        {
                            var answer = new AnswerDTO
                            {
                                Content = GetCellValueAsString(answerContentCell),
                                IsChecked = GetCellValueAsString(isCheckedCell).Equals("Yes", System.StringComparison.OrdinalIgnoreCase)
                            };
                            currentQuestion.Answers.Add(answer);
                        }
                    }
                }
            }

            return questions;
        }

        private string GetCellValueAsString(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    return cell.CellFormula;
                default:
                    return string.Empty;
            }
        }

        public async Task<IActionResult> OnPostExportQuestions()
        {
            var questions = GetQuestionsFromSomeSource(int.Parse(TestID));
            var thisTest = _dbContext.ClassSubjectTests.Where(x => x.TestId == int.Parse(TestID)).First();
            string fileName = TextConvert.RemoveDiacritics(thisTest.TestName) + "_Bo-cau-hoi.xlsx";

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/downloads", fileName);
            ExportQuestionsToExcel(questions, filePath);

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        private List<Question> GetQuestionsFromSomeSource(int testId)
        {
            List<Question> questions = new List<Question>();
            var selectedQuestions = _dbContext.TestQuestions.Where(x => x.TestId == testId).Include(x => x.Answers).ToList();
            foreach (var item in selectedQuestions)
            {
                Question newQuestion = new Question();
                newQuestion.QuestionText = item.Content;
                foreach (var ans in item.Answers)
                {
                    newQuestion.Answers.Add(new AnswerDTO()
                    {
                        Content = ans.Content,
                        IsChecked = ans.IsCorrectAnswer
                    });
                }
                questions.Add(newQuestion);
            }
            return questions;
        }

        public void ExportQuestionsToExcel(List<Question> questions, string filePath)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Questions");

            int rowIndex = 0;
            IRow headerRow = sheet.CreateRow(rowIndex++);
            headerRow.CreateCell(0).SetCellValue("Câu hỏi");
            headerRow.CreateCell(1).SetCellValue("Câu trả lời");
            headerRow.CreateCell(2).SetCellValue("Câu trả lời đúng");

            foreach (var question in questions)
            {
                bool isFirstAnswer = true;
                foreach (var answer in question.Answers)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    if (isFirstAnswer)
                    {
                        row.CreateCell(0).SetCellValue(question.QuestionText);
                        isFirstAnswer = false;
                    }
                    else
                    {
                        row.CreateCell(0).SetCellValue(string.Empty);
                    }
                    row.CreateCell(1).SetCellValue(answer.Content);
                    row.CreateCell(2).SetCellValue(answer.IsChecked ? "Yes" : "No");
                }
            }

            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }

    }


    public class AnswerDTO
    {
        public bool IsChecked { get; set; }
        public string Content { get; set; }
        public string AnswerId { get; set; }
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }
}
