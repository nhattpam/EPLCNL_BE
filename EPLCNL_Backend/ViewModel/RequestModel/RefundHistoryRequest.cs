using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class RefundHistoryRequest
    {
        public Guid? RefundRequestId { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
    }
}
