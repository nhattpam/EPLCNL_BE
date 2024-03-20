using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentsService
{
    public interface IAssignmentService
    {
        public Task<List<AssignmentResponse>> GetAll();
        public Task<AssignmentResponse> Get(Guid id);
        public Task<AssignmentResponse> Create(AssignmentRequest request);
        public Task<AssignmentResponse> Update(Guid id, AssignmentRequest request);
        public Task<AssignmentResponse> Delete(Guid id);
        public Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByAssignment(Guid id);
       

    }
}