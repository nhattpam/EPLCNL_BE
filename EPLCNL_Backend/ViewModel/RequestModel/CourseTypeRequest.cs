using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class CourseTypeRequest
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
