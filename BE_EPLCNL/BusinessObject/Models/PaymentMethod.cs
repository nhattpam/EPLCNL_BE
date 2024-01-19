using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
