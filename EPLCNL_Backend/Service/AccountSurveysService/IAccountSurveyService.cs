using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AccountSurveysService
{
    public interface IAccountSurveyService
    {
        Task<List<AccountSurveyResponse>> GetAll();
        Task<AccountSurveyResponse> Create(AccountSurveyRequest request);
        Task<AccountSurveyResponse> Update(Guid id, AccountSurveyRequest request);
        Task<AccountSurveyResponse> Delete(Guid id);
    }
}