using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class ReportRequest
    {
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReportedDate { get; set; }
    }
}
