using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? TransactionDate { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
        public bool? RefundStatus { get; set; }
    }
}
