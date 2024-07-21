using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class TestQuestion
    {
        public TestQuestion()
        {
            Answers = new HashSet<Answer>();
            StudentTestAnswers = new HashSet<StudentTestAnswer>();
        }

        public int TestId { get; set; }
        public int TestQuestionId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual ClassSubjectTest Test { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<StudentTestAnswer> StudentTestAnswers { get; set; }
    }
}
