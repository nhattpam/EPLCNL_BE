using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Violation
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ViolatedDate { get; set; }

        public virtual Course? Course { get; set; }
    }
}
