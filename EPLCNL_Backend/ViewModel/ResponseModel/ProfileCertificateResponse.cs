using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ResponseModel
{
    public class ProfileCertificateResponse
    {
        public Guid Id { get; set; }
        public Guid? LearnerId { get; set; }
        public Guid? CertificateId { get; set; }
        public string? Status { get; set; }
    }
}
