using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ConversationMember
    {
        public int ConversationMemberId { get; set; }
        public int AccountId { get; set; }
        public int ConversationId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual SubjectConversation Conversation { get; set; } = null!;
    }
}
