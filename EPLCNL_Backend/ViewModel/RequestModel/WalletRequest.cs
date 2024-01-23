using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class WalletRequest
    {
        public decimal? Balance { get; set; }
        public Guid? AccountId { get; set; }
    }
}
