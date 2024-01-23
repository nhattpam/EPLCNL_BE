using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CoursesService
{
    public interface ICourseService
    {
        public Task<List<CourseResponse>> GetCourses();

        public Task<CourseResponse> Create(CourseRequest request);

        public Task<CourseResponse> Delete(Guid id);

        public Task<CourseResponse> Update(Guid id, CourseRequest request);
    }
}
