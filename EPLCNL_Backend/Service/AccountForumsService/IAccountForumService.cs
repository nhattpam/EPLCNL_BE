using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AccountForumsService
{
    public interface IAccountForumService
    {
        Task<List<AccountForumResponse>> GetAll();
        Task<AccountForumResponse> Create(AccountForumRequest request);
        Task<AccountForumResponse> Update(Guid id, AccountForumRequest request);
        Task<AccountForumResponse> Delete(Guid id);
    }
}