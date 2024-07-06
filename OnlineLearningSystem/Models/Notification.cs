using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationOfAccounts = new HashSet<NotificationOfAccount>();
        }

        public int NotificationId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual Account CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<NotificationOfAccount> NotificationOfAccounts { get; set; }
    }
}
