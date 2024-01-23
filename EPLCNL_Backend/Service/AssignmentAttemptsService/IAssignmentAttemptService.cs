using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentAttemptsService
{
    public interface IAssignmentAttemptService
    {
        Task<List<AssignmentAttemptResponse>> GetAssignmentAttempts();
        Task<AssignmentAttemptResponse> Create(AssignmentAttemptRequest request);
        Task<AssignmentAttemptResponse> Update(Guid id, AssignmentAttemptRequest request);
        Task<AssignmentAttemptResponse> Delete(Guid id);
    }
}