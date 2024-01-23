using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CourseTypesService
{
    public interface ICourseTypeService
    {
        public Task<List<CourseTypeResponse>> GetCourseTypes();

        public Task<CourseTypeResponse> Create(CourseTypeRequest request);

        public Task<CourseTypeResponse> Delete(Guid id);

        public Task<CourseTypeResponse> Update(Guid id, CourseTypeRequest request);
    }
}
