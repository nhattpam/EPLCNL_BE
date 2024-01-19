using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            RefundRequests = new HashSet<RefundRequest>();
        }

        public Guid Id { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? CourseId { get; set; }
        public bool? RefundStatus { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Course? Course { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<RefundRequest> RefundRequests { get; set; }
    }
}
