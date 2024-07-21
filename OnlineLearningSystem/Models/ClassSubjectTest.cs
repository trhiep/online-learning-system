using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassSubjectTest
    {
        public ClassSubjectTest()
        {
            StudentTestAnswers = new HashSet<StudentTestAnswer>();
            TestQuestions = new HashSet<TestQuestion>();
        }

        public int TestId { get; set; }
        public int ClassSubjectId { get; set; }
        public string TestName { get; set; } = null!;
        public string? TestDescription { get; set; }
        public int Attempts { get; set; }
        public float Duration { get; set; }
        public float PassScore { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ClassSubject ClassSubject { get; set; } = null!;
        public virtual ICollection<StudentTestAnswer> StudentTestAnswers { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
