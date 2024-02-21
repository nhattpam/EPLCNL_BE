using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class AssignmentAttemptResponse
    {
        public Guid Id { get; set; }
        public Guid? AssignmentId { get; set; }
        public Guid? LearnerId { get; set; }
        public string? AnswerText { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public double? TotalGrade { get; set; }

        public virtual AssignmentResponse? Assignment { get; set; }
        public virtual LearnerResponse? Learner { get; set; }
    }
}
