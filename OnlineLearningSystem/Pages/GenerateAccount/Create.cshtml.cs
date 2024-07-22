using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Pages.ClassSubjectTests;

namespace OnlineLearningSystem.Pages.GenerateAccount
{
    public class CreateModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public CreateModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Accounts == null || Account == null)
            {
                return Page();
            }

            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }




        //-----------------
        [BindProperty]
        public IFormFile ExcelFile { get; set; } = default!;
        [BindProperty]
        public string TestID { get; set; } = default!;
        public async Task<IActionResult> OnPostImportAccount()
        {
            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                TempData["ToastMessage"] = "File Excel không được để trống!";
                return RedirectToPage("/GenerateAccount/Create");
            }

            // Read file excel here
            var accounts = await ReadExcelAsync(ExcelFile);
            foreach (var item in accounts)
            {
              
                _context.Accounts.Add(item);
                await _context.SaveChangesAsync();


               
                
            }
            TempData["ToastMessage"] = "Nhập câu hỏi từ file Excel thành công!";
            return RedirectToPage("/GenerateAccount/Index");
        }

        private async Task<List<Account>> ReadExcelAsync(IFormFile excelFile)
        {
            var accountList = new List<Account>();
            

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    Account account = new Account();
                    IRow currentRow = sheet.GetRow(row);
                    if (currentRow != null)
                    {
                        
                        ICell fullNameCell = currentRow.GetCell(0);
                        ICell emailCell = currentRow.GetCell(1);

                        account.Fullname = fullNameCell.ToString();
                        account.Username = "test";
                        account.Email = emailCell.ToString();
                        account.Password = "abcd1234";
                        account.Role = "Student";
                        account.Status = true;
                        accountList.Add(account);
                   
                    }
                    
                }
            }

            return accountList;
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
    }
}
