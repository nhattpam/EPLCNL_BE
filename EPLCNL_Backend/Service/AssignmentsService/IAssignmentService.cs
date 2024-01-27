using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentsService
{
    public interface IAssignmentService
    {
        Task<List<AssignmentResponse>> GetAll();
        Task<AssignmentResponse> Get(Guid id);
        Task<AssignmentResponse> Create(AssignmentRequest request);
        Task<AssignmentResponse> Update(Guid id, AssignmentRequest request);
        Task<AssignmentResponse> Delete(Guid id);
    }
}