using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class WalletHistory
    {
        public Guid Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Note { get; set; }
        public Guid? WalletId { get; set; }

        public virtual Wallet? Wallet { get; set; }
    }
}
