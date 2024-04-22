using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ClassLessonResponse
    {
        public Guid Id { get; set; }
        public string? ClassHours { get; set; }
        public string? ClassUrl { get; set; }
        public Guid? ClassModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

    }
}
