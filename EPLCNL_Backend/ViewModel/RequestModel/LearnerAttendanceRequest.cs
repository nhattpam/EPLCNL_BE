using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class LearnerAttendanceRequest
    {
        public Guid? AttendanceId { get; set; }
        public Guid? LearnerId { get; set; }
        public bool? Attended { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
