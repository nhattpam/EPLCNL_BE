using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            RefundRequests = new HashSet<RefundRequest>();
        }

        public Guid Id { get; set; }
        public Guid? TransactionId { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public string? Status { get; set; }
        public double? TotalGrade { get; set; }

        public virtual Transaction? Transaction { get; set; }
        public virtual ICollection<RefundRequest> RefundRequests { get; set; }
    }
}
