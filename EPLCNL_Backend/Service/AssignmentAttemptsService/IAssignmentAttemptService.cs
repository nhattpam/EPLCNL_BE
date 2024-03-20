using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentAttemptsService
{
    public interface IAssignmentAttemptService
    {
        public Task<List<AssignmentAttemptResponse>> GetAll();
        public Task<AssignmentAttemptResponse> Get(Guid id);
        public Task<AssignmentAttemptResponse> Create(AssignmentAttemptRequest request);
        public Task<AssignmentAttemptResponse> Update(Guid id, AssignmentAttemptRequest request);
        public Task<AssignmentAttemptResponse> Delete(Guid id);
        public Task<List<PeerReviewResponse>> GetAllPeerReviewsByAssignmentAttempt(Guid id);
        public Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByAssignmentNotLoggedInLearner(Guid assignmentId, Guid learnerId);
        public Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByAssignmentLoggedInLearnerNotGradeYet(Guid assignmentId, Guid learnerId);


    }
}