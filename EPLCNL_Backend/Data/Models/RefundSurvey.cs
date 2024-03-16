using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class RefundSurvey
    {
        public Guid Id { get; set; }
        public Guid? RefundRequestId { get; set; }
        public string? Reason { get; set; }

        public virtual RefundRequest? RefundRequest { get; set; }
    }
}
