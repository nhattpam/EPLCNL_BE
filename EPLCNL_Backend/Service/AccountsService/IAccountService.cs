using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AccountsService
{
    public interface IAccountService
    {
        Task<List<AccountResponse>> GetAccounts();
        Task<AccountResponse> Create(AccountRequest request);
        Task<AccountResponse> Update(Guid id, AccountRequest request);
        Task<AccountResponse> Delete(Guid id);
    }
}