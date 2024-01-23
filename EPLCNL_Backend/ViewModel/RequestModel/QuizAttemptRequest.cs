using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class QuizAttemptRequest
    {
        public Guid? QuizId { get; set; }
        public Guid? LearnerId { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public double? TotalGrade { get; set; }
    }
}
