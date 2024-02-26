using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TransactionsService
{
    public interface ITransactionService
    {
        Task<List<TransactionResponse>> GetAll();
        Task<TransactionResponse> Get(Guid id);
        Task<List<TransactionResponse>> GetTransactionsByLearner(Guid lid);
        Task<TransactionResponse> Create(TransactionRequest request);
        Task<TransactionResponse> Update(Guid id, TransactionRequest request);
        Task<TransactionResponse> Delete(Guid id);
    }
}