using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.Classrooms
{
    public class ListModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;


        public ListModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string searchClassText { get; set; }

        [BindProperty]
        public Classroom NewClassroomData { get; set; } = default!;

        [BindProperty]
        public Classroom SelectedClassroomData { get; set; } = default!;

        public IList<Classroom> Classroom { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Classrooms != null)
            {
                Classroom = await _context.Classrooms
                .Include(c => c.CreateByNavigation)
                .Include(c => c.FormTeacher).ToListAsync();
            }
        }

        public IActionResult OnGetCreateClassData()
        {

            var formTeacherList = _context.Accounts.Where(a => a.Role == "Teacher" && !_context.Classrooms.Any(c => c.FormTeacherId == a.AccountId)).ToList();

            List<string> formTeacherIdOptions = new List<string> { "<option value='0'>Chọn 1 giáo viên</option>" };
            if (formTeacherList.Any())
            {
                formTeacherIdOptions.AddRange(formTeacherList.Select(a => $"<option value='{a.AccountId}'>{a.Fullname}</option>").ToList());
            }
            var data = new
            {
                formTeacherIdOptions = formTeacherIdOptions
            };

            return new JsonResult(data);
        }

        public async Task<IActionResult> OnGetUpdateClassDataAsync(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.Include(a => a.FormTeacher).FirstOrDefaultAsync(m => m.ClassId == id);
            if (classroom == null)
            {
                return NotFound();
            }
            var formTeacherList = _context.Accounts.Where(a => a.Role == "Teacher" && !_context.Classrooms.Any(c => c.FormTeacherId == a.AccountId)).ToList();

            List<string> formTeacherIdOptions = new List<string> { "<option value='0'>Chọn 1 giáo viên</option>" };
            if (formTeacherList.Any())
            {
                formTeacherIdOptions.AddRange(formTeacherList.Select(a => $"<option value='{a.AccountId}'>{a.Fullname}</option>").ToList());
            }
            if (classroom.FormTeacherId != null)
            {
                formTeacherIdOptions.Add($"<option value='{classroom.FormTeacherId}'>{classroom.FormTeacher?.Fullname}</option>");
            }
            var response = new
            {
                classData = new
                {
                    formTeacherIdOptions = formTeacherIdOptions
                },
                selectedClassData = new
                {
                    classId = classroom.ClassId,
                    className = classroom.ClassName,
                    formTeacherId = classroom.FormTeacherId,
                    isActive = classroom.IsActive,

                }
            };

            SelectedClassroomData = classroom;

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            if (string.IsNullOrEmpty(NewClassroomData.ClassName))
            {
                var errors = "The ClassName field is required";
                return new JsonResult(new { success = false, message = errors });
            }
            else if (_context.Classrooms == null || NewClassroomData == null)
            {
                var errors = "Invalid data";
                return new JsonResult(new { success = false, message = errors });
            }
            var username = HttpContext.Session.GetString("UserSession");
            var CreatedBy = _context.Accounts.FirstOrDefault(a => a.Username == username);
            Console.WriteLine(username);
            NewClassroomData.CreateBy = CreatedBy.AccountId;
            NewClassroomData.FormTeacherId = NewClassroomData.FormTeacherId == 0 ? (int?)null : NewClassroomData.FormTeacherId;
            _context.Classrooms.Add(NewClassroomData);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (string.IsNullOrEmpty(SelectedClassroomData.ClassName))
            {
                var errors = "The ClassName field is required";
                return new JsonResult(new { success = false, message = errors });
            }
            else if (_context.Classrooms == null || SelectedClassroomData == null)
            {
                var errors = "Invalid data";
                return new JsonResult(new { success = false, message = errors });
            }

            SelectedClassroomData.FormTeacherId = SelectedClassroomData.FormTeacherId == 0 ? (int?)null : SelectedClassroomData.FormTeacherId;

            _context.Entry(SelectedClassroomData).Property(c => c.ClassName).IsModified = true;
            _context.Entry(SelectedClassroomData).Property(c => c.FormTeacherId).IsModified = true;
            _context.Entry(SelectedClassroomData).Property(c => c.IsActive).IsModified = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(SelectedClassroomData.ClassId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { success = true });
        }

        public void OnPostSearchClass()
        {

            if (string.IsNullOrEmpty(searchClassText))
            {
                Classroom = _context.Classrooms.ToList();
            }
            else
            {
                Classroom = _context.Classrooms
                    .Where(c => c.ClassName.Contains(searchClassText.Trim()))
                    .ToList();
            }
        }



        private bool ClassroomExists(int id)
        {
            return (_context.Classrooms?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }

    }
}
