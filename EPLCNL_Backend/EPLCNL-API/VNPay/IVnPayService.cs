using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.VNPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
