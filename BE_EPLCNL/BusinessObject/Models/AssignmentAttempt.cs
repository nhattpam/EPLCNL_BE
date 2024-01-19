using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class AssignmentAttempt
    {
        public Guid Id { get; set; }
        public Guid? AssignmentId { get; set; }
        public Guid? AccountId { get; set; }
        public string? AnswerText { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Assignment? Assignment { get; set; }
    }
}
