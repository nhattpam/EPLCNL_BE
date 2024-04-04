using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Attendance
    {
        public Attendance()
        {
            LearnerAttendances = new HashSet<LearnerAttendance>();
        }

        public Guid Id { get; set; }
        public Guid? ClassModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ClassModule? ClassModule { get; set; }
        public virtual ICollection<LearnerAttendance> LearnerAttendances { get; set; }
    }
}
