using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class NotificationOfAccount
    {
        public int NotificationId { get; set; }
        public int To { get; set; }
        public bool IsRead { get; set; }

        public virtual Notification Notification { get; set; } = null!;
        public virtual Account ToNavigation { get; set; } = null!;
    }
}
