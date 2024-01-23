using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.RequestModel
{
    public class CertificateCourseRequest
    {
        public Guid? CertificateId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Description { get; set; }
    }
}
