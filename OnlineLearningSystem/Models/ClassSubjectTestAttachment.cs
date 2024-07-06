using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassSubjectTestAttachment
    {
        public int TestAttachmentId { get; set; }
        public int TestId { get; set; }
        public string AttachmentLink { get; set; } = null!;

        public virtual ClassSubjectTest Test { get; set; } = null!;
    }
}
