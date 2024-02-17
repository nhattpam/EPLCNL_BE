using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class ViolationRequest
    {
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ViolatedDate { get; set; }
    }
}
