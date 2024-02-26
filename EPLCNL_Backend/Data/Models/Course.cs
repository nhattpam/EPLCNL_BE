using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Course
    {
        public Course()
        {
            CertificateCourses = new HashSet<CertificateCourse>();
            ClassModules = new HashSet<ClassModule>();
            Enrollments = new HashSet<Enrollment>();
            Feedbacks = new HashSet<Feedback>();
            Modules = new HashSet<Module>();
            Reports = new HashSet<Report>();
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? TutorId { get; set; }
        public decimal? StockPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnlineClass { get; set; }
        public double? Rating { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Tags { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Note { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public virtual Forum? Forum { get; set; }
        public virtual ICollection<CertificateCourse> CertificateCourses { get; set; }
        public virtual ICollection<ClassModule> ClassModules { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
