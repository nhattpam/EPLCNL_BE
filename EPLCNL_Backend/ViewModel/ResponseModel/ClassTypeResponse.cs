using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ClassTypeResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CourseId { get; set; }
    }
}
