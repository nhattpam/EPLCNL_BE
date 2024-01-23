using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class LessonResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? VideoUrl { get; set; }
        public string? Reading { get; set; }
        public Guid? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
