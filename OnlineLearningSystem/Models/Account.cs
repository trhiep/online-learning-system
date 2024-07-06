using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class Account
    {
        public Account()
        {
            ClassStudents = new HashSet<ClassStudent>();
            ClassSubjects = new HashSet<ClassSubject>();
            ClassroomCreateByNavigations = new HashSet<Classroom>();
            ClassroomFormTeachers = new HashSet<Classroom>();
            NotificationOfAccounts = new HashSet<NotificationOfAccount>();
            Notifications = new HashSet<Notification>();
            PostComments = new HashSet<PostComment>();
            StudentTestAnswers = new HashSet<StudentTestAnswer>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }
        public int StatusId { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual UserStatus Status { get; set; } = null!;
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<Classroom> ClassroomCreateByNavigations { get; set; }
        public virtual ICollection<Classroom> ClassroomFormTeachers { get; set; }
        public virtual ICollection<NotificationOfAccount> NotificationOfAccounts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<StudentTestAnswer> StudentTestAnswers { get; set; }
    }
}
