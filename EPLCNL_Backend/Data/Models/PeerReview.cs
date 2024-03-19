using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class PeerReview
    {
        public Guid Id { get; set; }
        public Guid? AssignmentAttemptId { get; set; }
        public Guid? LearnerId { get; set; }
        public double? Grade { get; set; }

        public virtual AssignmentAttempt? AssignmentAttempt { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
