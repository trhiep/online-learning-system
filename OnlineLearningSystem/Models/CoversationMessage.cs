using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class CoversationMessage
    {
        public int MessageId { get; set; }
        public int ConversationId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendTime { get; set; }
        public int SendById { get; set; }

        public virtual SubjectConversation Conversation { get; set; } = null!;
        public virtual Account SendBy { get; set; } = null!;
    }
}
