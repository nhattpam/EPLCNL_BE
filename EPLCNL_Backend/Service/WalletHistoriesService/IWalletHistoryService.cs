using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.WalletHistoriesService
{
    public interface IWalletHistoryService
    {
        public Task<List<WalletHistoryResponse>> GetAll();

        public Task<WalletHistoryResponse> Get(Guid id);

        public Task<WalletHistoryResponse> Create(WalletHistoryRequest request);

        public Task<WalletHistoryResponse> Delete(Guid id);

        public Task<WalletHistoryResponse> Update(Guid id, WalletHistoryRequest request);
    }
}