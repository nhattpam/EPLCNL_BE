using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class QuizAttempt
    {
        public Guid Id { get; set; }
        public Guid? QuizId { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Quiz? Quiz { get; set; }
    }
}
