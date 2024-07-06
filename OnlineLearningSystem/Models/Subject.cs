using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class Subject
    {
        public Subject()
        {
            ClassSubjects = new HashSet<ClassSubject>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;

        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
