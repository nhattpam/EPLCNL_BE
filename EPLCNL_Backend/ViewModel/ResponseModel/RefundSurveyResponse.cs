using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class RefundSurveyResponse
    {
        public Guid Id { get; set; }
        public Guid? RefundRequestId { get; set; }
        public string? Reason { get; set; }

        public virtual RefundResponse? RefundRequest { get; set; }
    }
}
