using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Data.Models
{
    public partial class EPLCNLContext : DbContext
    {
        public EPLCNLContext()
        {
        }

        public EPLCNLContext(DbContextOptions<EPLCNLContext> options)
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
        public virtual DbSet<ClassMaterial> ClassMaterials { get; set; } = null!;
        public virtual DbSet<ClassModule> ClassModules { get; set; } = null!;
        public virtual DbSet<ClassPractice> ClassPractices { get; set; } = null!;
        public virtual DbSet<ClassTopic> ClassTopics { get; set; } = null!;
        public virtual DbSet<ClassType> ClassTypes { get; set; } = null!;
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
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", true, true);
                IConfigurationRoot configuration = builder.Build();
                string _connectionString = configuration.GetConnectionString("MyCnn");
                optionsBuilder.UseSqlServer(_connectionString);
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
                    .HasConstraintName("FK__Account__role_id__03F0984C");
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
                    .HasConstraintName("FK__AccountFo__forum__0A9D95DB");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__AccountFo__learn__0C85DE4D");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.AccountForums)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__AccountFo__tutor__0B91BA14");
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
                    .HasConstraintName("FK__AccountSu__learn__2180FB33");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.AccountSurveys)
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK__AccountSu__surve__22751F6C");
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CloseTime).HasColumnName("close_time");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Deadline).HasColumnName("deadline");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.OpenTime).HasColumnName("open_time");

                entity.Property(e => e.QuestionText)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("question_text");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Assignmen__modul__151B244E");
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
                    .HasConstraintName("FK__Assignmen__assig__1AD3FDA4");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.AssignmentAttempts)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Assignmen__learn__1BC821DD");
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
                    .HasConstraintName("FK__Center__account___08B54D69");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Center__Staff_id__2CF2ADDF");
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
                    .HasConstraintName("FK__Certifica__certi__0D7A0286");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CertificateCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Certifica__cours__0E6E26BF");
            });

            modelBuilder.Entity<ClassLesson>(entity =>
            {
                entity.ToTable("ClassLesson");

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
                    .WithMany(p => p.ClassLessons)
                    .HasForeignKey(d => d.ClassModuleId)
                    .HasConstraintName("FK__ClassLess__class__2645B050");
            });

            modelBuilder.Entity<ClassMaterial>(entity =>
            {
                entity.ToTable("ClassMaterial");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassTopicId).HasColumnName("class_topic_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

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
                    .WithMany(p => p.ClassMaterials)
                    .HasForeignKey(d => d.ClassTopicId)
                    .HasConstraintName("FK__ClassMate__class__25518C17");
            });

            modelBuilder.Entity<ClassModule>(entity =>
            {
                entity.ToTable("ClassModule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassTypeId).HasColumnName("class_type_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.ClassType)
                    .WithMany(p => p.ClassModules)
                    .HasForeignKey(d => d.ClassTypeId)
                    .HasConstraintName("FK__ClassModu__class__2739D489");
            });

            modelBuilder.Entity<ClassPractice>(entity =>
            {
                entity.ToTable("ClassPractice");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassTopicId).HasColumnName("class_topic_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.ClassTopic)
                    .WithMany(p => p.ClassPractices)
                    .HasForeignKey(d => d.ClassTopicId)
                    .HasConstraintName("FK__ClassPrac__class__282DF8C2");
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
                    .HasConstraintName("FK__ClassTopi__class__245D67DE");
            });

            modelBuilder.Entity<ClassType>(entity =>
            {
                entity.ToTable("ClassType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ClassTypes)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__ClassType__cours__236943A5");
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
                    .HasConstraintName("FK__Course__category__208CD6FA");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__Course__tutor_id__07C12930");
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
                    .HasConstraintName("FK__Enrollmen__cours__06CD04F7");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Enrollmen__learn__05D8E0BE");
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
                    .HasConstraintName("FK__Feedback__course__1F98B2C1");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Feedback__learne__1EA48E88");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("Forum");

                entity.HasIndex(e => e.CourseId, "UQ__Forum__8F1EF7AF3F51E869")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.Forum)
                    .HasForeignKey<Forum>(d => d.CourseId)
                    .HasConstraintName("FK__Forum__course_id__09A971A2");
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
                    .HasConstraintName("FK__Learner__account__2B0A656D");
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
                    .HasConstraintName("FK__Lesson__module_i__123EB7A3");
            });

            modelBuilder.Entity<LessonMaterial>(entity =>
            {
                entity.ToTable("LessonMaterial");

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

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonMaterials)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK__LessonMat__lesso__19DFD96B");
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
                    .HasConstraintName("FK__Module__course_i__160F4887");
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
                    .HasConstraintName("FK__PaperWork__paper__1CBC4616");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.PaperWorks)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__PaperWork__tutor__1DB06A4F");
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
                    .HasConstraintName("FK__ProfileCe__certi__10566F31");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.ProfileCertificates)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__ProfileCe__learn__0F624AF8");
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
                    .HasConstraintName("FK__Question__quiz_i__17036CC0");
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
                    .HasConstraintName("FK__QuestionA__quest__114A936A");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassPracticeId).HasColumnName("class_practice_id");

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
                    .HasConstraintName("FK__Quiz__class_prac__14270015");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Quiz__module_id__1332DBDC");
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

                entity.Property(e => e.CloseTime).HasColumnName("close_time");

                entity.Property(e => e.LearnerId).HasColumnName("learner_id");

                entity.Property(e => e.OpenTime).HasColumnName("open_time");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.TotalGrade).HasColumnName("total_grade");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__QuizAttem__learn__18EBB532");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__QuizAttem__quiz___17F790F9");
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
                    .HasConstraintName("FK__RefundReq__trans__02FC7413");
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
                    .HasConstraintName("FK__Transacti__cours__01142BA1");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK__Transacti__learn__00200768");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Transacti__payme__02084FDA");
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
                    .HasConstraintName("FK__Tutor__account_i__29221CFB");

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK__Tutor__center_id__2BFE89A6");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Tutor__Staff_id__2DE6D218");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.HasIndex(e => e.AccountId, "UQ__Wallet__46A222CCC3492CBA")
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
                    .HasConstraintName("FK__Wallet__account___04E4BC85");
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
                    .HasConstraintName("FK__Staff__account_i__2A164134");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
