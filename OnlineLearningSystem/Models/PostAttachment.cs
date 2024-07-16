using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class PostAttachment
    {
        public int PostAttachmentId { get; set; }
        public int PostId { get; set; }
        public string AttachmentLink { get; set; } = null!;

        public virtual ClassSubjectPost Post { get; set; } = null!;
    }
}
