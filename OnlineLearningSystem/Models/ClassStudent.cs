using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassStudent
    {
        public int ClassStudentId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }

        public virtual Classroom Class { get; set; } = null!;
        public virtual Account Student { get; set; } = null!;
    }
}
