using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ProfileCertificate
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CertificateId { get; set; }
        public string? Status { get; set; }

        public virtual Certificate? Certificate { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
