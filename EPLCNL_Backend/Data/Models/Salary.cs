using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Salary
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Note { get; set; }

        public virtual Account? Account { get; set; }
    }
}
