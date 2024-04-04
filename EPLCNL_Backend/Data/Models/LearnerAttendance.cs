using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class LearnerAttendance
    {
        public Guid Id { get; set; }
        public Guid? AttendanceId { get; set; }
        public Guid? LearnerId { get; set; }
        public bool? Attended { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Attendance? Attendance { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
