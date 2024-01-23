using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class AssignmentAttempt
    {
        public Guid Id { get; set; }
        public Guid? AssignmentId { get; set; }
        public Guid? LearnerId { get; set; }
        public string? AnswerText { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Assignment? Assignment { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
