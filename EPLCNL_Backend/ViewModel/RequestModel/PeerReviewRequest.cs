using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class PeerReviewRequest
    {
        public Guid? AssignmentAttemptId { get; set; }
        public Guid? LearnerId { get; set; }
        public double? Grade { get; set; }

    }
}
