using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            ProfileCertificates = new HashSet<ProfileCertificate>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? CourseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<ProfileCertificate> ProfileCertificates { get; set; }
    }
}
