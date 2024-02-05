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
        public virtual DbSet<AccountSurvey> AccountSurveys { get; set; } = null!;
        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<AssignmentAttempt> AssignmentAttempts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Center> Centers { get; set; } = null!;
        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<CertificateCourse> CertificateCourses { get; set; } = null!;
        public virtual DbSet<ClassLesson> ClassLessons { get; set; } = null!;
        public virtual DbSet<ClassModule> ClassModules { get; set; } = null!;
        public virtual DbSet<ClassPractice> ClassPractices { get; set; } = null!;
        public virtual DbSet<ClassTopic> ClassTopics { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Forum> Forums { get; set; } = null!;
        public virtual DbSet<Learner> Learners { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<LessonMaterial> LessonMaterials { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<PaperWork> PaperWorks { get; set; } = null!;
        public virtual DbSet<PaperWorkType> PaperWorkTypes { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<ProfileCertificate> ProfileCertificates { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;
        public virtual DbSet<RefundRequest> RefundRequests { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Survey> Surveys { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<Tutor> Tutors { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:27.70.138.88,1433;Initial Catalog=nhatpmse;Persist Security Info=False;User ID=nhatpmse;Password=nhatpmse123456789:));MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30");
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

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

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
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__role_id__0F8D3381");
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
                    .HasConstraintName("FK__AccountFo__forum__163A3110");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__AccountFo__learn__18227982");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__AccountFo__tutor__172E5549");
            });

            modelBuilder.Entity<AccountSurvey>(entity =>
            {
                entity.ToTable("AccountSurvey");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Answer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.SurveyId).HasColumnName("survey_id");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AccountSurveys)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__AccountSu__learn__30EE274C");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.AccountSurveys)
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK__AccountSu__surve__31E24B85");
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

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Assignmen__modul__20B7BF83");
            });

            modelBuilder.Entity<AssignmentAttempt>(entity =>
            {
                entity.ToTable("AssignmentAttempt");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerText)
                    .HasColumnType("text")
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
                    .HasConstraintName("FK__Assignmen__assig__267098D9");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AssignmentAttempts)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Assignmen__learn__2764BD12");
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
                    .HasMaxLength(255)
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

                entity.Property(e => e.StaffId).HasColumnName("Staff_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Center__account___1451E89E");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Center__Staff_id__3B6BB5BF");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

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
            });

            modelBuilder.Entity<CertificateCourse>(entity =>
            {
                entity.ToTable("CertificateCourse");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.CertificateCourses)
                    .HasForeignKey(d => d.CertificateId)
                    .HasConstraintName("FK__Certifica__certi__19169DBB");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CertificateCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Certifica__cours__1A0AC1F4");
            });

            modelBuilder.Entity<ClassLesson>(entity =>
            {
                entity.ToTable("ClassLesson");

                entity.HasIndex(e => e.ClassModuleId, "UQ__ClassLes__C6F5C3FB1C4C8513")
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
                    .HasConstraintName("FK__ClassLess__class__35B2DC69");
            });

            modelBuilder.Entity<ClassModule>(entity =>
            {
                entity.ToTable("ClassModule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ClassModules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__ClassModu__cours__32D66FBE");
            });

            modelBuilder.Entity<ClassPractice>(entity =>
            {
                entity.ToTable("ClassPractice");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ClassTopic>(entity =>
            {
                entity.ToTable("ClassTopic");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassLessonId).HasColumnName("class_lesson_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.MaterialUrl)
                    .IsUnicode(false)
                    .HasColumnName("material_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.ClassLesson)
                    .WithMany(p => p.ClassTopics)
                    .HasForeignKey(d => d.ClassLessonId)
                    .HasConstraintName("FK__ClassTopi__class__33CA93F7");
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
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsOnlineClass).HasColumnName("is_online_class");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

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
                    .HasConstraintName("FK__Course__category__2FFA0313");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__Course__tutor_id__135DC465");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.EnrolledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("enrolled_date");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalGrade).HasColumnName("total_grade");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Enrollmen__cours__1269A02C");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Enrollmen__learn__11757BF3");
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

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("image_url");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Feedback__course__2F05DEDA");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Feedback__learne__2E11BAA1");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("Forum");

                entity.HasIndex(e => e.CourseId, "UQ__Forum__8F1EF7AF27B7636D")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.Forum)
                    .HasForeignKey<Forum>(d => d.CourseId)
                    .HasConstraintName("FK__Forum__course_id__15460CD7");
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
                    .HasConstraintName("FK__Learner__account__39836D4D");
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
                    .HasColumnType("text")
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
                    .HasConstraintName("FK__Lesson__module_i__1DDB52D8");
            });

            modelBuilder.Entity<LessonMaterial>(entity =>
            {
                entity.ToTable("LessonMaterial");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassTopicId).HasColumnName("class_topic_id");

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

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.ClassTopic)
                    .WithMany(p => p.LessonMaterials)
                    .HasForeignKey(d => d.ClassTopicId)
                    .HasConstraintName("FK__LessonMat__class__34BEB830");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonMaterials)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK__LessonMat__lesso__257C74A0");
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
                    .HasConstraintName("FK__Module__course_i__21ABE3BC");
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
                    .HasMaxLength(255)
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
                    .HasConstraintName("FK__PaperWork__paper__2C29722F");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.PaperWorks)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__PaperWork__tutor__2D1D9668");
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
                    .HasConstraintName("FK__ProfileCe__certi__1BF30A66");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.ProfileCertificates)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__ProfileCe__learn__1AFEE62D");
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
                    .HasConstraintName("FK__Question__quiz_i__22A007F5");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("QuestionAnswer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerText)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("answer_text");

                entity.Property(e => e.IsAnswer).HasColumnName("is_answer");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__QuestionA__quest__1CE72E9F");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassPracticeId).HasColumnName("class_practice_id");

                entity.Property(e => e.ClassTopicId).HasColumnName("class_topic_id");

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

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.ClassPractice)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ClassPracticeId)
                    .HasConstraintName("FK__Quiz__class_prac__36A700A2");

                entity.HasOne(d => d.ClassTopic)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ClassTopicId)
                    .HasConstraintName("FK__Quiz__class_topi__1FC39B4A");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Quiz__module_id__1ECF7711");
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
                    .HasConstraintName("FK__QuizAttem__learn__24885067");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__QuizAttem__quiz___23942C2E");
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

                entity.Property(e => e.Reason)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.RequestedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requested_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.RefundRequests)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK__RefundReq__trans__2B354DF6");
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

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.SurveyAnswer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("survey_answer");

                entity.Property(e => e.SurveyQuestion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("survey_question");
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

                entity.Property(e => e.RefundStatus).HasColumnName("refund_status");

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
                    .HasConstraintName("FK__Transacti__cours__294D0584");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Transacti__learn__2858E14B");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Transacti__payme__2A4129BD");
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
                    .HasConstraintName("FK__Tutor__account_i__379B24DB");

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK__Tutor__center_id__3A779186");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Tutor__Staff_id__3C5FD9F8");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.HasIndex(e => e.AccountId, "UQ__Wallet__46A222CCE96E2B50")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("balance");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.AccountId)
                    .HasConstraintName("FK__Wallet__account___108157BA");
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
                    .HasConstraintName("FK__Staff__account_i__388F4914");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
