using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class CertificateCourse
    {
        public Guid Id { get; set; }
        public Guid? CertificateId { get; set; }
        public Guid? CourseId { get; set; }
        public string? Description { get; set; }

        public virtual Certificate? Certificate { get; set; }
        public virtual Course? Course { get; set; }
    }
}
