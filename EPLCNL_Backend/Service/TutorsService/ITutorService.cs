using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TutorService
{
    public interface ITutorService
    {
        Task<List<TutorResponse>> GetAll();
        Task<TutorResponse> Get(Guid id);

        Task<TutorResponse> Create(TutorRequest request);
        Task<TutorResponse> Update(Guid id, TutorRequest request);
        Task<TutorResponse> Delete(Guid id);
    }
}