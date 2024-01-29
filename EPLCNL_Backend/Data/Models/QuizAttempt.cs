using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class QuizAttempt
    {
        public Guid Id { get; set; }
        public Guid? QuizId { get; set; }
        public Guid? LearnerId { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Learner? Learner { get; set; }
        public virtual Quiz? Quiz { get; set; }
    }
}
