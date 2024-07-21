using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Pages.ClassSubjects
{
    public static class CommonMethod
    {
        public static bool IsFormTeacher(OLS_DBContext context, int teacherID, int classID)
        {
            var classRoom = context.Classrooms
                .Where(c => c.ClassId == classID && c.FormTeacherId == teacherID)
                .FirstOrDefault();

            return classRoom != null;
        }

        public static bool IsStudentOfClass(OLS_DBContext context, int studentID, int classID)
        {
            var classStudent = context.ClassStudents
                .Where(c => c.ClassId == classID && c.StudentId == studentID)
                .FirstOrDefault();

            return classStudent != null;
        }
    }
}
