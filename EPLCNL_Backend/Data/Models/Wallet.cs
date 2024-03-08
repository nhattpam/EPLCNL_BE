using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            WalletHistories = new HashSet<WalletHistory>();
        }

        public Guid Id { get; set; }
        public decimal? Balance { get; set; }
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<WalletHistory> WalletHistories { get; set; }
    }
}
