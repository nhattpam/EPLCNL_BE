using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ViolationResponse
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ViolatedDate { get; set; }

        public virtual CourseResponse? Course { get; set; }
    }
}
