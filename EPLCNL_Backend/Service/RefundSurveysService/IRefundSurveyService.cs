using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.RefundSurveysService
{
    public interface IRefundSurveyService
    {
        public Task<List<RefundSurveyResponse>> GetAll();

        public Task<RefundSurveyResponse> Get(Guid id);

        public Task<RefundSurveyResponse> Create(RefundSurveyRequest request);

        public Task<RefundSurveyResponse> Delete(Guid id);

        public Task<RefundSurveyResponse> Update(Guid id, RefundSurveyRequest request);
    }
}
