using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.QuestionAnswersService
{
    public interface IQuestionAnswerService
    {
        public Task<List<QuestionAnswerResponse>> GetAll();

        public Task<QuestionAnswerResponse> Get(Guid id);

        public Task<QuestionAnswerResponse> Create(QuestionAnswerRequest request);

        public Task<QuestionAnswerResponse> Delete(Guid id);

        public Task<QuestionAnswerResponse> Update(Guid id, QuestionAnswerRequest request);
    }
}
