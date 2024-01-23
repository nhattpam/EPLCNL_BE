using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class FeedbackRequest
    {
        public string? FeedbackContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CourseId { get; set; }
    }
}
