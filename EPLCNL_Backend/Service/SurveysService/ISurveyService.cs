using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.SurveysService
{
    public interface ISurveyService
    {
        public Task<List<SurveyResponse>> GetAll();

        public Task<SurveyResponse> Get(Guid id);

        public Task<SurveyResponse> Create(SurveyRequest request);

        public Task<SurveyResponse> Delete(Guid id);

        public Task<SurveyResponse> Update(Guid id, SurveyRequest request);
    }
}
