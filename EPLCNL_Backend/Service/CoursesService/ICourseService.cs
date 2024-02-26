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
        public Task<List<CourseResponse>> GetAll();
        public Task<CourseResponse> Get(Guid id);
        Task<List<ModuleResponse>> GetAllModulesByCourse(Guid id);
        Task<List<ClassModuleResponse>> GetAllClassModulesByCourse(Guid id);
        Task<List<ReportResponse>> GetAllReportsByCourse(Guid id);

        public Task<CourseResponse> Create(CourseRequest request);

        public Task<CourseResponse> Delete(Guid id);

        public Task<CourseResponse> Update(Guid id, CourseRequest request);
    }
}
