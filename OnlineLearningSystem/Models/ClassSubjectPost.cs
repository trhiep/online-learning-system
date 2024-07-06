using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassSubjectPost
    {
        public ClassSubjectPost()
        {
            PostComments = new HashSet<PostComment>();
        }

        public int ClassSubjectId { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime PostedDate { get; set; }

        public virtual ClassSubject ClassSubject { get; set; } = null!;
        public virtual PostAttachment? PostAttachment { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}
