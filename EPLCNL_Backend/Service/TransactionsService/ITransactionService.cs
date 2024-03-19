using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TransactionsService
{
    public interface ITransactionService
    {
        public Task<List<TransactionResponse>> GetAll();
        public Task<TransactionResponse> Get(Guid id);
        public Task<TransactionResponse> Create(TransactionRequest request);
        public Task<TransactionResponse> Update(Guid id, TransactionRequest request);
        public Task<TransactionResponse> Delete(Guid id);
        public Task<bool> PayByWallet(Guid id);
    }
}