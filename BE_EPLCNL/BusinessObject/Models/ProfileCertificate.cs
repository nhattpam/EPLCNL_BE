using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ProfileCertificate
    {
        public Guid? AccountId { get; set; }
        public Guid? CertificateId { get; set; }
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Certificate? Certificate { get; set; }
    }
}
