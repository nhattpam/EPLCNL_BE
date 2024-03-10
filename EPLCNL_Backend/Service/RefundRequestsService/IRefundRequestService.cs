using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ResponseModel;

namespace Service.RefundRequestsService
{
    public interface IRefundRequestService
    {
        public Task<List<RefundResponse>> GetAll();

        public Task<RefundResponse> Get(Guid id);

        public Task<List<RefundHistoryResponse>> GetRefundHistoryByRefundRequest(Guid id);

        public Task<RefundResponse> Create(ViewModel.RequestModel.RefundRequest request);

        public Task<RefundResponse> Delete(Guid id);

        public Task<RefundResponse> Update(Guid id, ViewModel.RequestModel.RefundRequest request);
    }
}
