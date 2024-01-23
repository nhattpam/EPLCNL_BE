using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentsService
{
    public interface IAssignmentService
    {
        Task<List<AssignmentResponse>> GetAssignments();
        Task<AssignmentResponse> Create(AssignmentRequest request);
        Task<AssignmentResponse> Update(Guid id, AssignmentRequest request);
        Task<AssignmentResponse> Delete(Guid id);
    }
}