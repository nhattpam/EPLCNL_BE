using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ProfileCertificatesService
{
    public interface IProfileCertificateService
    {
        public Task<List<ProfileCertificateResponse>> GetAll();

        public Task<ProfileCertificateResponse> Create(ProfileCertificateRequest request);

        public Task<ProfileCertificateResponse> Delete(Guid id);

        public Task<ProfileCertificateResponse> Update(Guid id, ProfileCertificateRequest request);
    }
}
