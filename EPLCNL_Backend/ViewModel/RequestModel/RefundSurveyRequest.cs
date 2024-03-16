using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class RefundSurveyRequest
    {
        public Guid? RefundRequestId { get; set; }
        public string? Reason { get; set; }

    }
}
