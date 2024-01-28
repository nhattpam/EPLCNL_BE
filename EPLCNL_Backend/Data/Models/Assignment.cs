using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Assignment
    {
        public Assignment()
        {
            AssignmentAttempts = new HashSet<AssignmentAttempt>();
        }

        public Guid Id { get; set; }
        public string? QuestionText { get; set; }
        public TimeSpan? Deadline { get; set; }
        public Guid? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Module? Module { get; set; }
        public virtual ICollection<AssignmentAttempt> AssignmentAttempts { get; set; }
    }
}
