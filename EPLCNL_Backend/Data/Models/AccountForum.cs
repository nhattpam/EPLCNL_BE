using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class AccountForum
    {
        public Guid? LearnerId { get; set; }
        public Guid? TutorId { get; set; }
        public Guid? ForumId { get; set; }
        public string? Message { get; set; }
        public DateTime? MessagedDate { get; set; }

        public virtual Forum? Forum { get; set; }
        public virtual Learner? Learner { get; set; }
        public virtual Tutor? Tutor { get; set; }
    }
}
