using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class QuizAttemptResponse
    {
        public Guid Id { get; set; }
        public Guid? QuizId { get; set; }
        public Guid? LearnerId { get; set; }
        public DateTime? AttemptedDate { get; set; }
        public double? TotalGrade { get; set; }
        public virtual LearnerResponse? Learner { get; set; }
        public virtual QuizResponse? Quiz { get; set; }
    }
}
