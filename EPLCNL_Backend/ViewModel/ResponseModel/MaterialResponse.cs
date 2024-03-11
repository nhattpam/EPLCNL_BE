using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class MaterialResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? MaterialUrl { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? ClassTopicId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TopicResponse? ClassTopic { get; set; }
        public virtual LessonResponse? Lesson { get; set; }
    }
}
