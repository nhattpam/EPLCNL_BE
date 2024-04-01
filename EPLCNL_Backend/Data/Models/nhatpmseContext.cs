using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class nhatpmseContext : DbContext
    {
        public nhatpmseContext()
        {
        }

        public nhatpmseContext(DbContextOptions<nhatpmseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountForum> AccountForums { get; set; } = null!;
        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<AssignmentAttempt> AssignmentAttempts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Center> Centers { get; set; } = null!;
        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<ClassLesson> ClassLessons { get; set; } = null!;
        public virtual DbSet<ClassModule> ClassModules { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Forum> Forums { get; set; } = null!;
        public virtual DbSet<Learner> Learners { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<PaperWork> PaperWorks { get; set; } = null!;
        public virtual DbSet<PaperWorkType> PaperWorkTypes { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<PeerReview> PeerReviews { get; set; } = null!;
        public virtual DbSet<ProfileCertificate> ProfileCertificates { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;
        public virtual DbSet<RefundHistory> RefundHistories { get; set; } = null!;
        public virtual DbSet<RefundRequest> RefundRequests { get; set; } = null!;
        public virtual DbSet<RefundSurvey> RefundSurveys { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<Tutor> Tutors { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WalletHistory> WalletHistories { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:171.248.85.240,1433;Initial Catalog=nhatpmse;Persist Security Info=False;User ID=nhatpmse;Password=nhatpmse123456789:));MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__role_id__69D26A44");
            });

            modelBuilder.Entity<AccountForum>(entity =>
            {
                entity.ToTable("AccountForum");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ForumId).HasColumnName("forum_id");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.MessagedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("messaged_date");

                entity.Property(e => e.TutorId).HasColumnName("tutor_id");

                entity.HasOne(d => d.Forum)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.ForumId)
                    .HasConstraintName("FK__AccountFo__forum__6F8B439A");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__AccountFo__learn__71738C0C");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__AccountFo__tutor__707F67D3");
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Deadline).HasColumnName("deadline");

                entity.Property(e => e.GradeToPass).HasColumnName("grade_to_pass");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.QuestionText)
                    .IsUnicode(false)
                    .HasColumnName("question_text");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Assignmen__modul__7914ADD4");
            });

            modelBuilder.Entity<AssignmentAttempt>(entity =>
            {
                entity.ToTable("AssignmentAttempt");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerText)
                    .IsUnicode(false)
                    .HasColumnName("answer_text");

                entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");

                entity.Property(e => e.AttemptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("attempted_date");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.TotalGrade).HasColumnName("total_grade");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentAttempts)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK__Assignmen__assig__7ECD872A");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AssignmentAttempts)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Assignmen__learn__7FC1AB63");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Center>(entity =>
            {
                entity.ToTable("Center");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.StaffId).HasColumnName("Staff_id");

                entity.Property(e => e.TaxIdentificationNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tax_identification_number");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Center__account___6DA2FB28");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Center__Staff_id__12D47FD7");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");

                entity.HasIndex(e => e.CourseId, "UQ__Certific__8F1EF7AFCBB419C5")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.Certificate)
                    .HasForeignKey<Certificate>(d => d.CourseId)
                    .HasConstraintName("FK__Certifica__cours__7267B045");
            });

            modelBuilder.Entity<ClassLesson>(entity =>
            {
                entity.ToTable("ClassLesson");

                entity.HasIndex(e => e.ClassModuleId, "UQ__ClassLes__C6F5C3FBDF237F65")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("class_hours");

                entity.Property(e => e.ClassModuleId).HasColumnName("class_module_id");

                entity.Property(e => e.ClassUrl)
                    .IsUnicode(false)
                    .HasColumnName("class_url");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.ClassModule)
                    .WithOne(p => p.ClassLesson)
                    .HasForeignKey<ClassLesson>(d => d.ClassModuleId)
                    .HasConstraintName("FK__ClassLess__class__0E0FCABA");
            });

            modelBuilder.Entity<ClassModule>(entity =>
            {
                entity.ToTable("ClassModule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ClassModules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__ClassModu__cours__0B335E0F");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsOnlineClass).HasColumnName("is_online_class");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.StockPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("stock_price");

                entity.Property(e => e.Tags)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tags");

                entity.Property(e => e.TutorId).HasColumnName("tutor_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Course__category__0A3F39D6");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__Course__tutor_id__6CAED6EF");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.HasIndex(e => e.TransactionId, "UQ__Enrollme__85C600AECB342E8C")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EnrolledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("enrolled_date");

                entity.Property(e => e.RefundStatus).HasColumnName("refund_status");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalGrade).HasColumnName("total_grade");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.HasOne(d => d.Transaction)
                    .WithOne(p => p.Enrollment)
                    .HasForeignKey<Enrollment>(d => d.TransactionId)
                    .HasConstraintName("FK__Enrollmen__trans__6BBAB2B6");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.FeedbackContent)
                    .IsUnicode(false)
                    .HasColumnName("feedback_content");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Feedback__course__094B159D");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Feedback__learne__0856F164");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("Forum");

                entity.HasIndex(e => e.CourseId, "UQ__Forum__8F1EF7AF702C33FB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.Forum)
                    .HasForeignKey<Forum>(d => d.CourseId)
                    .HasConstraintName("FK__Forum__course_id__6E971F61");
            });

            modelBuilder.Entity<Learner>(entity =>
            {
                entity.ToTable("Learner");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Learners)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Learner__account__10EC3765");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Reading)
                    .IsUnicode(false)
                    .HasColumnName("reading");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.Property(e => e.VideoUrl)
                    .IsUnicode(false)
                    .HasColumnName("video_url");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Lesson__module_i__76384129");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.LessonId).HasColumnName("lesson_id");

                entity.Property(e => e.MaterialUrl)
                    .IsUnicode(false)
                    .HasColumnName("material_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK__Material__lesson__7DD962F1");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Material__topic___0D1BA681");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Module__course_i__7A08D20D");
            });

            modelBuilder.Entity<PaperWork>(entity =>
            {
                entity.ToTable("PaperWork");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PaperWorkTypeId).HasColumnName("paper_work_type_id");

                entity.Property(e => e.PaperWorkUrl)
                    .IsUnicode(false)
                    .HasColumnName("paper_work_url");

                entity.Property(e => e.TutorId).HasColumnName("tutor_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.PaperWorkType)
                    .WithMany(p => p.PaperWorks)
                    .HasForeignKey(d => d.PaperWorkTypeId)
                    .HasConstraintName("FK__PaperWork__paper__066EA8F2");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.PaperWorks)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__PaperWork__tutor__0762CD2B");
            });

            modelBuilder.Entity<PaperWorkType>(entity =>
            {
                entity.ToTable("PaperWorkType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PeerReview>(entity =>
            {
                entity.ToTable("PeerReview");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AssignmentAttemptId).HasColumnName("assignment_attempt_id");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.HasOne(d => d.AssignmentAttempt)
                    .WithMany(p => p.PeerReviews)
                    .HasForeignKey(d => d.AssignmentAttemptId)
                    .HasConstraintName("FK__PeerRevie__assig__01A9F3D5");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.PeerReviews)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__PeerRevie__learn__00B5CF9C");
            });

            modelBuilder.Entity<ProfileCertificate>(entity =>
            {
                entity.ToTable("ProfileCertificate");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.ProfileCertificates)
                    .HasForeignKey(d => d.CertificateId)
                    .HasConstraintName("FK__ProfileCe__certi__744FF8B7");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.ProfileCertificates)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__ProfileCe__learn__735BD47E");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.DefaultGrade).HasColumnName("default_grade");

                entity.Property(e => e.QuestionAudioUrl)
                    .IsUnicode(false)
                    .HasColumnName("question_audio_url");

                entity.Property(e => e.QuestionImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("question_image_url");

                entity.Property(e => e.QuestionText)
                    .IsUnicode(false)
                    .HasColumnName("question_text");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__Question__quiz_i__7AFCF646");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("QuestionAnswer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerText)
                    .IsUnicode(false)
                    .HasColumnName("answer_text");

                entity.Property(e => e.IsAnswer).HasColumnName("is_answer");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__QuestionA__quest__75441CF0");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Deadline).HasColumnName("deadline");

                entity.Property(e => e.GradeToPass).HasColumnName("grade_to_pass");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Quiz__module_id__772C6562");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Quiz__topic_id__7820899B");
            });

            modelBuilder.Entity<QuizAttempt>(entity =>
            {
                entity.ToTable("QuizAttempt");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AttemptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("attempted_date");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.TotalGrade).HasColumnName("total_grade");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__QuizAttem__learn__7CE53EB8");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__QuizAttem__quiz___7BF11A7F");
            });

            modelBuilder.Entity<RefundHistory>(entity =>
            {
                entity.ToTable("RefundHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.RefundRequestId).HasColumnName("refund_request_id");

                entity.HasOne(d => d.RefundRequest)
                    .WithMany(p => p.RefundHistories)
                    .HasForeignKey(d => d.RefundRequestId)
                    .HasConstraintName("FK__RefundHis__refun__16A510BB");
            });

            modelBuilder.Entity<RefundRequest>(entity =>
            {
                entity.ToTable("RefundRequest");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_date");

                entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");

                entity.Property(e => e.RequestedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requested_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Enrollment)
                    .WithMany(p => p.RefundRequests)
                    .HasForeignKey(d => d.EnrollmentId)
                    .HasConstraintName("FK__RefundReq__enrol__057A84B9");
            });

            modelBuilder.Entity<RefundSurvey>(entity =>
            {
                entity.ToTable("RefundSurvey");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.RefundRequestId).HasColumnName("refund_request_id");

                entity.HasOne(d => d.RefundRequest)
                    .WithMany(p => p.RefundSurveys)
                    .HasForeignKey(d => d.RefundRequestId)
                    .HasConstraintName("FK__RefundSur__refun__188D592D");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reported_date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Report__course_i__14BCC849");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Report__learner___15B0EC82");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassLessonId).HasColumnName("class_lesson_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.ClassLesson)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.ClassLessonId)
                    .HasConstraintName("FK__Topic__class_les__0C278248");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Transacti__cours__03923C47");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Transacti__learn__029E180E");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Transacti__payme__04866080");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable("Tutor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CenterId).HasColumnName("center_id");

                entity.Property(e => e.IsFreelancer).HasColumnName("is_freelancer");

                entity.Property(e => e.StaffId).HasColumnName("Staff_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Tutor__account_i__0F03EEF3");

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK__Tutor__center_id__11E05B9E");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Tutor__Staff_id__13C8A410");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.HasIndex(e => e.AccountId, "UQ__Wallet__46A222CCD4A47E4F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("balance");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.AccountId)
                    .HasConstraintName("FK__Wallet__account___6AC68E7D");
            });

            modelBuilder.Entity<WalletHistory>(entity =>
            {
                entity.ToTable("WalletHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.WalletHistories)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK__WalletHis__walle__179934F4");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Staff__account_i__0FF8132C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
