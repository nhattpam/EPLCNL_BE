using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class QuizResponse
    {
        public Guid? ModuleId { get; set; }
        public Guid? ClassTopicId { get; set; }
        public Guid? ClassPracticeId { get; set; }
        public string? Name { get; set; }
        public double? GradeToPass { get; set; }
        public TimeSpan? Deadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ClassPracticeResponse? ClassPractice { get; set; }
        public virtual ClassTopicResponse? ClassTopic { get; set; }
        public virtual ModuleResponse? Module { get; set; }
    }
}
