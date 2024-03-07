using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class RefundHistory
    {
        public Guid Id { get; set; }
        public Guid? RefundRequestId { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }

        public virtual RefundRequest? RefundRequest { get; set; }
    }
}
