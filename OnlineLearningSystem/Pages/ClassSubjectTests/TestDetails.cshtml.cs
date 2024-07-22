using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Utils;
using OnlineLearningSystem.Utils.Converter;

namespace OnlineLearningSystem.Pages.ClassSubjectTests
{
    public class TestDetailsModel : PageModel
    {
        OLS_DBContext _dbContext;
        public Account LogedInAccount;

        public TestDetailsModel(OLS_DBContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public ClassSubjectTest ClassSubjectTest { get; set; } = default!;

        public List<TestResultDTO> TestResults { get; set; } = default!;
        public Dictionary<string, TestResultDTO> TestResultDic { get; set; } = default!;
        public bool IsAllowToEdit { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? testId)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                string username = HttpContext.Session.GetString("UserSession");
                LogedInAccount = _dbContext.Accounts.Where(x => x.Username == username).First();
                if (LogedInAccount != null){
                    if (testId != null)
                    {
                        ClassSubjectTest = await _dbContext.ClassSubjectTests
                            .Include(x => x.TestQuestions)
                            .Include(x => x.ClassSubject)
                            .Where(x => x.TestId == testId)
                            .FirstAsync();
                        if (ClassSubjectTest != null)
                        {
                            if (LogedInAccount.Role == "Student")
                            {
                                TestResults = new List<TestResultDTO>();
                                TestResults = GetTestResults(LogedInAccount.AccountId, ClassSubjectTest.TestId);
                                if (!TestResults.Any()) IsAllowToEdit = true;
                            } else
                            {
                                var studentsOfThisClass = _dbContext.ClassStudents
                                    .Include(x => x.Student)
                                    .Where(x => x.ClassId == ClassSubjectTest.ClassSubject.ClassId)
                                    .ToList();
                                if (studentsOfThisClass.Any())
                                {
                                    TestResultDic = new Dictionary<string, TestResultDTO>();
                                    foreach (var student in studentsOfThisClass)
                                    {
                                        var resultsOfStudent = GetTestResults(student.StudentId, ClassSubjectTest.TestId);
                                        TestResultDTO highestGradeResult = resultsOfStudent.OrderByDescending(tr => tr.Grade).FirstOrDefault();

                                        if (highestGradeResult != null)
                                        {
                                            TestResultDTO newStudentResult = new TestResultDTO()
                                            {
                                                AttemptsNo = highestGradeResult.AttemptsNo,
                                                TakenDate = highestGradeResult.TakenDate,
                                                CorrectAnswers = highestGradeResult.CorrectAnswers,
                                                Grade = highestGradeResult.Grade,
                                                Status = highestGradeResult.Status
                                            };
                                            TestResultDic.Add(student.Student.Username, newStudentResult);
                                        }
                                        
                                    }
                                    if (!TestResultDic.Any()) IsAllowToEdit = true;
                                }
                            }
                        }

                        else
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
            }
            return RedirectToPage("/Authen/Login");
        }


        private List<TestResultDTO> GetTestResults(int studentId, int testId)
        {
            // Lấy tổng số câu hỏi cho bài kiểm tra
            var totalQuestions = _dbContext.TestQuestions
                .Count(tq => tq.TestId == testId);

            // Lấy tất cả các câu trả lời đúng từ cơ sở dữ liệu và chuyển sang client-side
            var correctAnswers = _dbContext.Answers
                .Where(a => a.IsCorrectAnswer)
                .AsEnumerable() // Chuyển sang client-side để sử dụng GroupBy
                .GroupBy(a => a.QuestionId)
                .ToDictionary(g => g.Key, g => g.Select(a => a.QuestionAnswerId).ToList());

            // Lấy tất cả các câu trả lời của sinh viên từ cơ sở dữ liệu
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

                        // Kiểm tra xem học sinh đã chọn đúng các đáp án chính xác và không chọn quá nhiều hoặc ít hơn
                        if (studentAnswerIds.Count <= correctAnswerIds.Count && studentAnswerIds.All(id => correctAnswerIds.Contains(id)))
                        {
                            return 1;
                        }
                        return 0;
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
        public JsonResult OnPostFetchStudentGrades(string studentAccount, int testId)
        {
            if (studentAccount != null)
            {
                var thisAccount = _dbContext.Accounts.Where(x => x.Username == studentAccount).First();
                List<TestResultDTO> resultOfStudent = GetTestResults(thisAccount.AccountId, testId);
                return new JsonResult(new { success = true, resultOfStudent = resultOfStudent });
            }
            return new JsonResult(new { success = true});
        }

        [BindProperty]
        public string TestID { get; set; } = default!;
        public async Task<IActionResult> OnPostExportGrades()
        {
            var thisClassSubjecTest = _dbContext.ClassSubjectTests
                .Include(x => x.ClassSubject)
                .Where(x => x.TestId == int.Parse(TestID)).First();
            var studentsOfThisClass = _dbContext.ClassStudents
                                    .Include(x => x.Student)
                                    .Where(x => x.ClassId == thisClassSubjecTest.ClassSubject.ClassId)
                                    .ToList();

            List<TestResultOfStudentDTO> results = new List<TestResultOfStudentDTO>();
            foreach (var student in studentsOfThisClass)
            {
                results.AddRange(GetTestResultOfAstudent(student.StudentId, int.Parse(TestID)));
            }

            string fileName = TextConvert.RemoveDiacritics(thisClassSubjecTest.TestName) + "_Ket-qua-thi.xlsx";

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/downloads", fileName);
            ExportGradesToExcel(results, filePath);

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public async Task<IActionResult> OnPostExportDoTestHistory()
        {
            var thisTest = _dbContext.ClassSubjectTests.Where(x => x.TestId == int.Parse(TestID)).First();

            string username = HttpContext.Session.GetString("UserSession");
            var thisAccount = _dbContext.Accounts.Where(x => x.Username == username).First();


            string sourceFolder = Path.Combine(Directory.GetCurrentDirectory(), "bin/Debug/net6.0/Logs/" + TextConvert.RemoveDiacritics(thisTest.TestName));
            if (thisAccount.Role == "Student")
            {
                sourceFolder = Path.Combine(Directory.GetCurrentDirectory(), "bin/Debug/net6.0/Logs/" + TextConvert.RemoveDiacritics(thisTest.TestName)+"/"+username);
            }
            
            if (!Directory.Exists(sourceFolder))
            {
                Directory.CreateDirectory(sourceFolder);

                string sampleFilePath = Path.Combine(sourceFolder, "EMPTY_HISTORY.txt");
                System.IO.File.WriteAllText(sampleFilePath, "Không có lịch sử.");
            }

            string zipFilePath = Path.Combine(Path.GetTempPath(), TextConvert.RemoveDiacritics(thisTest.TestName) + "_Lich-su-lam-bai.zip");

            FileHelper.ZipFolder(sourceFolder, zipFilePath);

            var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);
            var fileName = TextConvert.RemoveDiacritics(thisTest.TestName) + "_Lich-su-lam-bai.zip";

            return File(fileBytes, "application/zip", fileName);
        }

        public void ExportGradesToExcel(List<TestResultOfStudentDTO> results, string filePath)
        {
            // Khởi tạo workbook
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Điểm thi");

            // Tạo header
            int rowIndex = 0;
            IRow headerRow = sheet.CreateRow(rowIndex++);
            headerRow.CreateCell(0).SetCellValue("Tài khoản");
            headerRow.CreateCell(1).SetCellValue("Họ tên");
            headerRow.CreateCell(2).SetCellValue("Ngày làm bài");
            headerRow.CreateCell(3).SetCellValue("Số câu đúng");
            headerRow.CreateCell(4).SetCellValue("Điểm");
            headerRow.CreateCell(5).SetCellValue("Kết quả");
            headerRow.CreateCell(6).SetCellValue("Ghi chú");

            // Duyệt danh sách và ghi vào sheet
            foreach (var result in results)
            {
                IRow dataRow = sheet.CreateRow(rowIndex++);

                dataRow.CreateCell(0).SetCellValue(result.AccountName);
                dataRow.CreateCell(1).SetCellValue(result.Fullname);
                dataRow.CreateCell(2).SetCellValue(result.TakenDate.ToString("dd/MM/yyyy : HH:mm"));
                dataRow.CreateCell(3).SetCellValue(result.CorrectAnswers);
                dataRow.CreateCell(4).SetCellValue(result.Grade);
                dataRow.CreateCell(5).SetCellValue(result.Status);
                dataRow.CreateCell(6).SetCellValue("Lần thi" + result.AttemptsNo.ToString());
            }

            // Ghi workbook vào file
            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }


        private List<TestResultOfStudentDTO> GetTestResultOfAstudent(int studentId, int testId)
        {
            // Lấy tổng số câu hỏi cho bài kiểm tra
            var totalQuestions = _dbContext.TestQuestions
                .Count(tq => tq.TestId == testId);

            // Lấy tất cả các câu trả lời đúng từ cơ sở dữ liệu và chuyển sang client-side
            var correctAnswers = _dbContext.Answers
                .Where(a => a.IsCorrectAnswer)
                .AsEnumerable() // Chuyển sang client-side để sử dụng GroupBy
                .GroupBy(a => a.QuestionId)
                .ToDictionary(g => g.Key, g => g.Select(a => a.QuestionAnswerId).ToList());

            // Lấy tất cả các câu trả lời của sinh viên từ cơ sở dữ liệu
            var studentAnswers = _dbContext.StudentTestAnswers
                .Include(x => x.Student)
                .Where(sta => sta.TestId == testId && sta.StudentId == studentId)
                .ToList();

            // Nhóm câu trả lời của sinh viên theo số lần thử
            var groupedByAttempts = studentAnswers
                .GroupBy(sta => sta.AttemptNo)
                .ToList();

            // Danh sách kết quả
            var testResults = new List<TestResultOfStudentDTO>();

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

                        // Kiểm tra xem học sinh đã chọn đúng các đáp án chính xác và không chọn quá nhiều hoặc ít hơn
                        if (studentAnswerIds.Count <= correctAnswerIds.Count && studentAnswerIds.All(id => correctAnswerIds.Contains(id)))
                        {
                            return 1;
                        }
                        return 0;
                    });

                // Tính điểm
                var score = correctCount * (10.0 / totalQuestions);

                // Xác định trạng thái (Ví dụ: "Passed" nếu điểm >= 5, ngược lại "Failed")
                var status = score >= 5 ? "ĐẠT" : "KHÔNG ĐẠT";

                // Map vào class TestResult
                var testResult = new TestResultOfStudentDTO
                {
                    AttemptsNo = attemptNo,
                    TakenDate = takenDate,
                    CorrectAnswers = correctCount,
                    Grade = (float)score,
                    Status = status,
                    AccountName = studentAnswers[0].Student.Username,
                    Fullname = studentAnswers[0].Student.Fullname,
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

    public class TestResultOfStudentDTO
    {
        public string AccountName { get; set; }
        public string Fullname { get; set; }
        public int AttemptsNo { get; set; }
        public DateTime TakenDate { get; set; }
        public int CorrectAnswers { get; set; }
        public float Grade { get; set; }
        public string Status { get; set; }
    }
}
