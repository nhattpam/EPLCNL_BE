using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class AssignmentAttemptRequest
    {
        public Guid? AssignmentId { get; set; }
        public Guid? LearnerId { get; set; }
        public string? AnswerText { get; set; }
        public double? TotalGrade { get; set; }
    }
}
