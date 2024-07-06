using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class StudentTestAnswer
    {
        public int StudentTestAnswerId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public int QuestionAnswerId { get; set; }
        public int TestQuestionId { get; set; }

        public virtual Answer QuestionAnswer { get; set; } = null!;
        public virtual Account Student { get; set; } = null!;
        public virtual ClassSubjectTest Test { get; set; } = null!;
        public virtual TestQuestion TestQuestion { get; set; } = null!;
    }
}
