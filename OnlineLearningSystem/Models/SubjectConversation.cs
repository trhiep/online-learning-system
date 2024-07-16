using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class SubjectConversation
    {
        public SubjectConversation()
        {
            ConversationMembers = new HashSet<ConversationMember>();
            CoversationMessages = new HashSet<CoversationMessage>();
        }

        public int ConversationId { get; set; }
        public int ClassSubjectId { get; set; }
        public string GroupChatName { get; set; } = null!;

        public virtual ClassSubject ClassSubject { get; set; } = null!;
        public virtual ICollection<ConversationMember> ConversationMembers { get; set; }
        public virtual ICollection<CoversationMessage> CoversationMessages { get; set; }
    }
}
