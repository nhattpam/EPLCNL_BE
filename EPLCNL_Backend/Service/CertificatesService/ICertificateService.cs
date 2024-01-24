using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CertificatesService
{
    public interface ICertificateService
    {
        public Task<List<CertificateResponse>> GetAll();

        public Task<CertificateResponse> Create(CertificateRequest request);

        public Task<CertificateResponse> Delete(Guid id);

        public Task<CertificateResponse> Update(Guid id, CertificateRequest request);
    }
}
