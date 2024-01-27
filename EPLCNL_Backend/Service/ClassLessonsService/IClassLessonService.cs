using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassLessonsService
{
    public interface IClassLessonService
    {
        public Task<List<ClassLessonResponse>> GetAll();

        public Task<ClassLessonResponse> Get(Guid id);

        public Task<ClassLessonResponse> Create(ClassLessonRequest request);

        public Task<ClassLessonResponse> Delete(Guid id);

        public Task<ClassLessonResponse> Update(Guid id, ClassLessonRequest request);
    }
}
