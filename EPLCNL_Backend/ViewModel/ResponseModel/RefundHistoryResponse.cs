using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;

namespace ViewModel.ResponseModel
{
    public class RefundHistoryResponse
    {
        public Guid Id { get; set; }
        public Guid? RefundRequestId { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }

        public virtual RefundRequest? RefundRequest { get; set; }
    }
}
