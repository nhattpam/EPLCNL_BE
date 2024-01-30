using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LessonsService
{
    public interface ILessonService
    {
        public Task<List<LessonResponse>> GetAll();

        public Task<LessonResponse> Get(Guid id);
        public Task<List<LessonMaterialResponse>> GetAllMaterialsByLesson(Guid id);
        public Task<LessonResponse> Create(LessonRequest request);

        public Task<LessonResponse> Delete(Guid id);

        public Task<LessonResponse> Update(Guid id, LessonRequest request);
    }
}
