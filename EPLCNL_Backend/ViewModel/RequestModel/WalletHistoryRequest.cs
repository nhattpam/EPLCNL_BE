using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class WalletHistoryRequest
    {
        public DateTime? TransactionDate { get; set; }
        public string? Note { get; set; }
        public Guid? WalletId { get; set; }
    }
}
