using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.WalletsService
{
    public interface IWalletService
    {
        Task<List<WalletResponse>> GetAll();
        Task<WalletResponse> Get(Guid id);
        Task<List<WalletHistoryResponse>> GetWalletHistoryByWallet(Guid id);
        Task<WalletResponse> Create(WalletRequest request);
        Task<WalletResponse> Update(Guid id, WalletRequest request);
        Task<WalletResponse> Delete(Guid id);
    }
}