using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class Answer
    {
        public Answer()
        {
            StudentTestAnswers = new HashSet<StudentTestAnswer>();
        }

        public int QuestionAnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public virtual TestQuestion Question { get; set; } = null!;
        public virtual ICollection<StudentTestAnswer> StudentTestAnswers { get; set; }
    }
}
