using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassSubject
    {
        public ClassSubject()
        {
            ClassSubjectPosts = new HashSet<ClassSubjectPost>();
            ClassSubjectTests = new HashSet<ClassSubjectTest>();
        }

        public int ClassSubjectId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SubjectTeacher { get; set; }

        public virtual Classroom Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Account SubjectTeacherNavigation { get; set; } = null!;
        public virtual ICollection<ClassSubjectPost> ClassSubjectPosts { get; set; }
        public virtual ICollection<ClassSubjectTest> ClassSubjectTests { get; set; }
    }
}
