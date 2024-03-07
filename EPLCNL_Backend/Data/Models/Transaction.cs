using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Learner? Learner { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual Enrollment? Enrollment { get; set; }

    }
}
