using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Accounts = new HashSet<Account>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
