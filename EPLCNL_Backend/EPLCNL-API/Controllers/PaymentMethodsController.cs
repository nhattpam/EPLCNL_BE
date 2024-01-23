using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaymentMethodsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/payment-methods")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }


        [HttpGet]
        public async Task<ActionResult<List<PaymentMethodResponse>>> GetAllPaymentMethods()
        {
            try
            {
                var rs = await _paymentMethodService.GetPaymentMethods();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethodResponse>> Create([FromBody] PaymentMethodRequest request)
        {
            try
            {
                var result = await _paymentMethodService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<PaymentMethodResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _paymentMethodService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<PaymentMethodResponse>> Update([FromQuery] Guid id, [FromBody] PaymentMethodRequest request)
        {
            try
            {
                var rs = await _paymentMethodService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
