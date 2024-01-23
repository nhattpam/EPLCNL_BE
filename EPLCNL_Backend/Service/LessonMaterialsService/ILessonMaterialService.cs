using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LessonMaterialsService
{
    public interface ILessonMaterialService
    {
        public Task<List<LessonMaterialResponse>> GetLessonMaterials();

        public Task<LessonMaterialResponse> Create(LessonMaterialRequest request);

        public Task<LessonMaterialResponse> Delete(Guid id);

        public Task<LessonMaterialResponse> Update(Guid id, LessonMaterialRequest request);
    }
}