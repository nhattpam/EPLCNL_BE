using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class FeedbackResponse
    {
        public Guid Id { get; set; }
        public string? FeedbackContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }

        public virtual CourseResponse? Course { get; set; }
        public virtual LearnerResponse? Learner { get; set; }
    }
}
