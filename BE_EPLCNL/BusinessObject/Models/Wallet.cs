using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Wallet
    {
        public Guid Id { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; }
    }
}
