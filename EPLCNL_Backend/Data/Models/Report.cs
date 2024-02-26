using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Report
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
