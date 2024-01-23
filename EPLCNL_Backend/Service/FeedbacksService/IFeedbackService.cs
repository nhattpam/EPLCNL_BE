using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.FeedbacksService
{
    public interface IFeedbackService
    {
        public Task<List<FeedbackResponse>> GetFeedbacks();

        public Task<FeedbackResponse> Create(FeedbackRequest request);

        public Task<FeedbackResponse> Delete(Guid id);

        public Task<FeedbackResponse> Update(Guid id, FeedbackRequest request);
    }
}
