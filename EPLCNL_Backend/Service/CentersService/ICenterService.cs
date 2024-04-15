using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CentersService
{
    public interface ICenterService
    {
        public Task<List<CenterResponse>> GetAll();
        public Task<CenterResponse> Get(Guid id);
        public Task<List<TutorResponse>> GetAllTutorsByCenter(Guid id);
        public Task<List<CourseResponse>> GetAllCoursesByCenter(Guid id);

        public Task<CenterResponse> Create(CenterRequest request);

        public Task<CenterResponse> Delete(Guid id);

        public Task<CenterResponse> Update(Guid id, CenterRequest request);
    }
}
