using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class TransactionRequest
    {
        public Guid? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
    }
}
