﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassStudents
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineLearningSystem.Models.OLS_DBContext _context;

        public DetailsModel(OnlineLearningSystem.Models.OLS_DBContext context)
        {
            _context = context;
        }

      public ClassStudent ClassStudent { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ClassStudents == null)
            {
                return NotFound();
            }

            var classstudent = await _context.ClassStudents.FirstOrDefaultAsync(m => m.ClassStudentId == id);
            if (classstudent == null)
            {
                return NotFound();
            }
            else 
            {
                ClassStudent = classstudent;
            }
            return Page();
        }
    }
}
