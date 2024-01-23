using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Enrollment
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public string? Status { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
