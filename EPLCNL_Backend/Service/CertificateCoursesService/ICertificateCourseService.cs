using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CertificateCoursesService
{
    public interface ICertificateCourseService
    {
        public Task<List<CertificateCourseResponse>> GetAll();

        public Task<CertificateCourseResponse> Create(CertificateCourseRequest request);

        public Task<CertificateCourseResponse> Delete(Guid id);

        public Task<CertificateCourseResponse> Update(Guid id, CertificateCourseRequest request);

    }
}
