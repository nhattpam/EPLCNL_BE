using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TransactionsService
{
    public interface ITransactionService
    {
        Task<List<TransactionResponse>> GetAll();
        Task<TransactionResponse> Create(TransactionRequest request);
        Task<TransactionResponse> Update(Guid id, TransactionRequest request);
        Task<TransactionResponse> Delete(Guid id);
    }
}