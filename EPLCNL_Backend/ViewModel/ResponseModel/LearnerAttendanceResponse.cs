using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class LearnerAttendanceResponse
    {
        public Guid Id { get; set; }
        public Guid? AttendanceId { get; set; }
        public Guid? LearnerId { get; set; }
        public bool? Attended { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AttendanceResponse? Attendance { get; set; }
        public virtual LearnerResponse? Learner { get; set; }
    }
}
