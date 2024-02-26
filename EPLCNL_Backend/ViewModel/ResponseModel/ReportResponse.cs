using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ReportResponse
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string? ImageUrl { get; set; }

        public virtual CourseResponse? Course { get; set; }
        public virtual LearnerResponse? Learner { get; set; }

    }
}
