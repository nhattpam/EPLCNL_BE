using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountSurveys = new HashSet<AccountSurvey>();
            AssignmentAttempts = new HashSet<AssignmentAttempt>();
            Courses = new HashSet<Course>();
            Enrollments = new HashSet<Enrollment>();
            Feedbacks = new HashSet<Feedback>();
            PaperWorks = new HashSet<PaperWork>();
            QuizAttempts = new HashSet<QuizAttempt>();
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid? RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Center? Center { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<AccountSurvey> AccountSurveys { get; set; }
        public virtual ICollection<AssignmentAttempt> AssignmentAttempts { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<PaperWork> PaperWorks { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
