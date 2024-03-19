using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class PeerReviewResponse
    {
        public Guid Id { get; set; }
        public Guid? AssignmentAttemptId { get; set; }
        public Guid? LearnerId { get; set; }
        public double? Grade { get; set; }

        public virtual AssignmentAttemptResponse? AssignmentAttempt { get; set; }
        public virtual LearnerResponse? Learner { get; set; }
    }
}
