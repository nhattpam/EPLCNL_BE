using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.RefundHistoriesService
{
    public interface IRefundHistoryService
    {
        public Task<List<RefundHistoryResponse>> GetAll();

        public Task<RefundHistoryResponse> Get(Guid id);

        public Task<RefundHistoryResponse> Create(RefundHistoryRequest request);

        public Task<RefundHistoryResponse> Delete(Guid id);

        public Task<RefundHistoryResponse> Update(Guid id, RefundHistoryRequest request);
    }
}