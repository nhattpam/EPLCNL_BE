using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class SalaryResponse
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Note { get; set; }

        public virtual AccountResponse? Account { get; set; }
    }
}
