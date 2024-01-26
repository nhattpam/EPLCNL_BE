using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class CloudFone
    {
        public string ApiKey { get; set; } = null!;
        public string? CallNumber { get; set; }
        public string? CallName { get; set; }
        public string? QueueNumber { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? Key { get; set; }
        public string? KeyRinging { get; set; }
        public string? Status { get; set; }
        public string? Direction { get; set; }
        public string? NumberPbx { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
    }
}
