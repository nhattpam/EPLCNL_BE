using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.QuizAttemptsService
{
    public interface IQuizAttemptService
    {
        public Task<List<QuizAttemptResponse>> GetQuizAttempts();

        public Task<QuizAttemptResponse> Create(QuizAttemptRequest request);

        public Task<QuizAttemptResponse> Delete(Guid id);

        public Task<QuizAttemptResponse> Update(Guid id, QuizAttemptRequest request);
    }
}
