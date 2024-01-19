using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class RefundRequest
    {
        public Guid Id { get; set; }
        public Guid? TransactionId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }

        public virtual Transaction? Transaction { get; set; }
    }
}
