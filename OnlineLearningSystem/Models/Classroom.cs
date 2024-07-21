using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            ClassStudents = new HashSet<ClassStudent>();
            ClassSubjects = new HashSet<ClassSubject>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int? CreateBy { get; set; }
        public int? FormTeacherId { get; set; }
        public bool IsActive { get; set; }

        public virtual Account? CreateByNavigation { get; set; } = null!;
        public virtual Account? FormTeacher { get; set; } = null!;
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
