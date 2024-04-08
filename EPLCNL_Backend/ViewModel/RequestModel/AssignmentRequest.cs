using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class AssignmentRequest
    {
        public string? QuestionText { get; set; }
        public string? QuestionAudioUrl { get; set; }

        public int? Deadline { get; set; }
        public Guid? ModuleId { get; set; }
        public Guid? TopicId { get; set; }

        public double? GradeToPass { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
