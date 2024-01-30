using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.QuizzesService
{
    public interface IQuizService
    {
        public Task<List<QuizResponse>> GetAll();

        public Task<QuizResponse> Get(Guid id);

        public Task<List<QuestionResponse>> GetAllQuestionsByQuiz(Guid id);


        public Task<QuizResponse> Create(QuizRequest request);

        public Task<QuizResponse> Delete(Guid id);

        public Task<QuizResponse> Update(Guid id, QuizRequest request);
    }
}
