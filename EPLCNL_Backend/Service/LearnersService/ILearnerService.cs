using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LearnersService
{
    public interface ILearnerService
    {
        public Task<List<LearnerResponse>> GetAll();

        public Task<LearnerResponse> Get(Guid? id);

        public Task<LearnerResponse> Create(LearnerRequest request);

        public Task<LearnerResponse> Delete(Guid id);

        public Task<LearnerResponse> Update(Guid id, LearnerRequest request);

        public Task<List<ForumResponse>> GetAllForumsByLearner(Guid id);
        public Task<List<EnrollmentResponse>> GetAllEnrollmentsByLearner(Guid id);
        public Task<List<TransactionResponse>> GetAllTransactionsByLearner(Guid lid);
        public Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByLearner(Guid learnerId);
        public Task<List<QuizAttemptResponse>> GetAllQuizAttemptsByLearner(Guid learnerId);
        public Task<List<RefundResponse>> GetAllRefundsByLearner(Guid id);
        public Task<List<ProfileCertificateResponse>> GetAllProfileCertificatesByLearner(Guid id);

    }
}
