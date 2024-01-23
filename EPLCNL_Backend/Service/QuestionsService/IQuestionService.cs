using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.QuestionsService
{
    public interface IQuestionService
    {
        public Task<List<QuestionResponse>> GetQuestions();

        public Task<QuestionResponse> Create(QuestionRequest request);

        public Task<QuestionResponse> Delete(Guid id);

        public Task<QuestionResponse> Update(Guid id, QuestionRequest request);
    }
}
