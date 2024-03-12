using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ClassModuleResponse
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CourseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual CourseResponse? Course { get; set; }

        public virtual ClassLessonResponse? ClassLesson { get; set; }

    }
}
