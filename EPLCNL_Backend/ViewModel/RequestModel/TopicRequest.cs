using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class TopicRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MaterialUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? ClassLessonId { get; set; }
    }
}
