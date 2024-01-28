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
        public TimeSpan? Deadline { get; set; }
        public Guid? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
