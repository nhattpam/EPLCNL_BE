using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            CertificateCourses = new HashSet<CertificateCourse>();
            ProfileCertificates = new HashSet<ProfileCertificate>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<CertificateCourse> CertificateCourses { get; set; }
        public virtual ICollection<ProfileCertificate> ProfileCertificates { get; set; }
    }
}
