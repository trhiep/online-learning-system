using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineLearningSystem.Models
{
    public partial class OLS_DBContext : DbContext
    {
        public OLS_DBContext()
        {
        }

        public OLS_DBContext(DbContextOptions<OLS_DBContext> options)
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
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<ConversationMember> ConversationMembers { get; set; } = null!;
        public virtual DbSet<CoversationMessage> CoversationMessages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationOfAccount> NotificationOfAccounts { get; set; } = null!;
        public virtual DbSet<PostAttachment> PostAttachments { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<StudentTestAnswer> StudentTestAnswers { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectConversation> SubjectConversations { get; set; } = null!;
        public virtual DbSet<TestQuestion> TestQuestions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=34.124.147.48;Initial Catalog=OLS_DB;User ID=admin;Password=12345678");
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

                entity.Property(e => e.Role)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
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

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

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

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

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

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.ClassSubjectTests)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassTest_ClassSubject");
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

            modelBuilder.Entity<ConversationMember>(entity =>
            {
                entity.ToTable("ConversationMember");

                entity.Property(e => e.ConversationMemberId).HasColumnName("ConversationMemberID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ConversationMembers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ConversationMember_Account");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.ConversationMembers)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ConversationMember_SubjectConversation");
            });

            modelBuilder.Entity<CoversationMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("CoversationMessage_pk");

                entity.ToTable("CoversationMessage");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.SendById).HasColumnName("SendByID");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.CoversationMessages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CoversationMessage_SubjectConversation");

                entity.HasOne(d => d.SendBy)
                    .WithMany(p => p.CoversationMessages)
                    .HasForeignKey(d => d.SendById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CoversationMessage_Account");
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
                entity.ToTable("PostAttachment");

                entity.Property(e => e.PostAttachmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("PostAttachmentID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostAttachments)
                    .HasForeignKey(d => d.PostId)
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

            modelBuilder.Entity<SubjectConversation>(entity =>
            {
                entity.HasKey(e => e.ConversationId)
                    .HasName("SubjectConversation_pk");

                entity.ToTable("SubjectConversation");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

                entity.Property(e => e.GroupChatName).HasMaxLength(100);

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.SubjectConversations)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubjectConversation_ClassSubject");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
