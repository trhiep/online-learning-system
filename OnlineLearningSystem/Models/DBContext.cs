using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineLearningSystem.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<ClassStudent> ClassStudents { get; set; } = null!;
        public virtual DbSet<ClassSubject> ClassSubjects { get; set; } = null!;
        public virtual DbSet<ClassSubjectPost> ClassSubjectPosts { get; set; } = null!;
        public virtual DbSet<ClassSubjectTest> ClassSubjectTests { get; set; } = null!;
        public virtual DbSet<ClassSubjectTestAttachment> ClassSubjectTestAttachments { get; set; } = null!;
        public virtual DbSet<ClassSubjectTestType> ClassSubjectTestTypes { get; set; } = null!;
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationOfAccount> NotificationOfAccounts { get; set; } = null!;
        public virtual DbSet<PostAttachment> PostAttachments { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<StudentTestAnswer> StudentTestAnswers { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<TestQuestion> TestQuestions { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(conf.GetConnectionString("DBContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account_UserRole");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account_UserStatus");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.QuestionAnswerId)
                    .HasName("Answer_pk");

                entity.ToTable("Answer");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Answer_TestQuestion");
            });

            modelBuilder.Entity<ClassStudent>(entity =>
            {
                entity.ToTable("ClassStudent");

                entity.Property(e => e.ClassStudentId).HasColumnName("ClassStudentID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassStudents)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassStudent_Classroom");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ClassStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassStudent_Account");
            });

            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.ToTable("ClassSubject");

                entity.Property(e => e.ClassSubjectId).HasColumnName("CLassSubjectID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassSubject_Classroom");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassSubject_Subject");

                entity.HasOne(d => d.SubjectTeacherNavigation)
                    .WithMany(p => p.ClassSubjects)
                    .HasForeignKey(d => d.SubjectTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassSubject_Account");
            });

            modelBuilder.Entity<ClassSubjectPost>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("ClassSubjectPost_pk");

                entity.ToTable("ClassSubjectPost");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.ClassSubjectId).HasColumnName("CLassSubjectID");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.ClassSubjectPosts)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassPost_ClassSubject");
            });

            modelBuilder.Entity<ClassSubjectTest>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("ClassSubjectTest_pk");

                entity.ToTable("ClassSubjectTest");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.ClassSubjectId).HasColumnName("CLassSubjectID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TestName).HasMaxLength(100);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.ClassSubjectTests)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassTest_ClassSubject");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ClassSubjectTests)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassSubjectTest_ClassSubjectTestType");
            });

            modelBuilder.Entity<ClassSubjectTestAttachment>(entity =>
            {
                entity.HasKey(e => e.TestAttachmentId)
                    .HasName("ClassSubjectTestAttachments_pk");

                entity.Property(e => e.TestAttachmentId).HasColumnName("TestAttachmentID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.ClassSubjectTestAttachments)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassSubjectTestAttachments_ClassSubjectTest");
            });

            modelBuilder.Entity<ClassSubjectTestType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("ClassSubjectTestType_pk");

                entity.ToTable("ClassSubjectTestType");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("TypeID");

                entity.Property(e => e.TypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("Classroom_pk");

                entity.ToTable("Classroom");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.FormTeacherId).HasColumnName("FormTeacherID");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ClassroomCreateByNavigations)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Classroom_Account_CreateBy");

                entity.HasOne(d => d.FormTeacher)
                    .WithMany(p => p.ClassroomFormTeachers)
                    .HasForeignKey(d => d.FormTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Classroom_Account_FormTeacher");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Notification_Account");
            });

            modelBuilder.Entity<NotificationOfAccount>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.To })
                    .HasName("NotificationOfAccount_pk");

                entity.ToTable("NotificationOfAccount");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationOfAccounts)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotificationOfAccount_Notification");

                entity.HasOne(d => d.ToNavigation)
                    .WithMany(p => p.NotificationOfAccounts)
                    .HasForeignKey(d => d.To)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotificationOfAccount_Account_To");
            });

            modelBuilder.Entity<PostAttachment>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PostAttachment_pk");

                entity.ToTable("PostAttachment");

                entity.Property(e => e.PostId)
                    .ValueGeneratedNever()
                    .HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithOne(p => p.PostAttachment)
                    .HasForeignKey<PostAttachment>(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostAttachment_ClassPost");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PostComment_pk");

                entity.ToTable("PostComment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.FatherId).HasColumnName("FatherID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostComment_ClassPost");

                entity.HasOne(d => d.PostedByNavigation)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostComment_Account");
            });

            modelBuilder.Entity<StudentTestAnswer>(entity =>
            {
                entity.ToTable("StudentTestAnswer");

                entity.Property(e => e.StudentTestAnswerId).HasColumnName("StudentTestAnswerID");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestQuestionId).HasColumnName("TestQuestionID");

                entity.HasOne(d => d.QuestionAnswer)
                    .WithMany(p => p.StudentTestAnswers)
                    .HasForeignKey(d => d.QuestionAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentTestAnswer_Answer");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTestAnswers)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentTestAnswer_Account");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.StudentTestAnswers)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentTestAnswer_ClassTest");

                entity.HasOne(d => d.TestQuestion)
                    .WithMany(p => p.StudentTestAnswers)
                    .HasForeignKey(d => d.TestQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentTestAnswer_TestQuestion");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.SubjectName).HasMaxLength(100);
            });

            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.ToTable("TestQuestion");

                entity.Property(e => e.TestQuestionId).HasColumnName("TestQuestionID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestQuestions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestQuestion_ClassTest");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("UserRole_pk");

                entity.ToTable("UserRole");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("UserStatus_pk");

                entity.ToTable("UserStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
