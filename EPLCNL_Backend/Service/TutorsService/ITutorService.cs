using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TutorService
{
    public interface ITutorService
    {
        Task<List<TutorResponse>> GetTutors();
        Task<TutorResponse> Create(TutorRequest request);
        Task<TutorResponse> Update(Guid id, TutorRequest request);
        Task<TutorResponse> Delete(Guid id);
    }
}