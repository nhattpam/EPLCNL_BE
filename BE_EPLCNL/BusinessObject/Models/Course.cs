using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Course
    {
        public Course()
        {
            ClassTypes = new HashSet<ClassType>();
            Enrollments = new HashSet<Enrollment>();
            Feedbacks = new HashSet<Feedback>();
            Modules = new HashSet<Module>();
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? AccountId { get; set; }
        public decimal? StockPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnlineClass { get; set; }
        public double? Rating { get; set; }
        public Guid? CoursePlatformId { get; set; }
        public string? Tags { get; set; }

        public virtual Account? Account { get; set; }
        public virtual CoursePlatform? CoursePlatform { get; set; }
        public virtual Forum? Forum { get; set; }
        public virtual ICollection<ClassType> ClassTypes { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
