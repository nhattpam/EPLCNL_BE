using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ModuleResponse
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AssignmentResponse> Assignments { get; set; }
        public virtual ICollection<LessonResponse> Lessons { get; set; }
        public virtual ICollection<QuizResponse> Quizzes { get; set; }
    }
}
