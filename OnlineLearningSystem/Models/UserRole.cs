using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
