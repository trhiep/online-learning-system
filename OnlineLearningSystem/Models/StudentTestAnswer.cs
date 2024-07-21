using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class StudentTestAnswer
    {
        public int StudentTestAnswerId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public int TestQuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
        public DateTime AnswerTime { get; set; }
        public int AttemptNo { get; set; }

        public virtual Answer SelectedAnswer { get; set; } = null!;
        public virtual Account Student { get; set; } = null!;
        public virtual ClassSubjectTest Test { get; set; } = null!;
        public virtual TestQuestion TestQuestion { get; set; } = null!;
    }
}
