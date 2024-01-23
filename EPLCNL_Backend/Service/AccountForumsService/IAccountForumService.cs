using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AccountForumsService
{
    public interface IAccountForumService
    {
        Task<List<AccountForumResponse>> GetAccountForums();
        Task<AccountForumResponse> Create(AccountForumRequest request);
        Task<AccountForumResponse> Update(Guid learnerid, Guid tutorid, AccountForumRequest request);
        Task<AccountForumResponse> Delete(Guid learnerid, Guid tutorid);
    }
}