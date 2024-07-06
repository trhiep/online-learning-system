using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class PostComment
    {
        public int PostId { get; set; }
        public long CommentId { get; set; }
        public int PostedBy { get; set; }
        public string CommentContent { get; set; } = null!;
        public int? FatherId { get; set; }

        public virtual ClassSubjectPost Post { get; set; } = null!;
        public virtual Account PostedByNavigation { get; set; } = null!;
    }
}
