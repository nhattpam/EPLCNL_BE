using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class RefundRequest
    {
        public RefundRequest()
        {
            RefundHistories = new HashSet<RefundHistory>();
        }

        public Guid Id { get; set; }
        public Guid? EnrollmentId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }

        public virtual Enrollment? Enrollment { get; set; }
        public virtual ICollection<RefundHistory> RefundHistories { get; set; }
    }
}
