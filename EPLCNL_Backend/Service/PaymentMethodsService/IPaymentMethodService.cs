using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.PaymentMethodsService
{
    public interface IPaymentMethodService
    {
        public Task<List<PaymentMethodResponse>> GetPaymentMethods();

        public Task<PaymentMethodResponse> Create(PaymentMethodRequest request);

        public Task<PaymentMethodResponse> Delete(Guid id);

        public Task<PaymentMethodResponse> Update(Guid id, PaymentMethodRequest request);
    }
}
