using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class WalletHistoryResponse
    {
        public Guid Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Note { get; set; }
        public Guid? WalletId { get; set; }

        public virtual WalletResponse? Wallet { get; set; }
    }
}
