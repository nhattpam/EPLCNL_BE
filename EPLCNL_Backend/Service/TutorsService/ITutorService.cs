using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TutorService
{
    public interface ITutorService
    {
        Task<List<TutorResponse>> GetAll();
        Task<TutorResponse> Get(Guid id);
        Task<List<CourseResponse>> GetAllCoursesByTutor(Guid id);
        Task<List<PaperWorkResponse>> GetAllPaperWorksByTutor(Guid id);
        Task<List<ForumResponse>> GetAllForumsByTutor(Guid id);

        Task<TutorResponse> Create(TutorRequest request);
        Task<TutorResponse> Update(Guid id, TutorRequest request);
        Task<TutorResponse> Delete(Guid id);
    }
}