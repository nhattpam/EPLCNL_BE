using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CentersService;
using Service.RefundRequestsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/refund-requests")]
    [ApiController]
    public class RefundRequestsController : ControllerBase
    {
        private readonly IRefundRequestService _refundRequestService;

        public RefundRequestsController(IRefundRequestService refundRequestService)
        {
            _refundRequestService = refundRequestService;
        }


        [HttpGet]
        public async Task<ActionResult<List<RefundResponse>>> GetAllRefundRequests()
        {
            try
            {
                var rs = await _refundRequestService.GetRefundRequests();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RefundResponse>> Create([FromBody] ViewModel.RequestModel.RefundRequest request)
        {
            try
            {
                var result = await _refundRequestService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<RefundResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _refundRequestService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<RefundResponse>> Update([FromQuery] Guid id, [FromBody] ViewModel.RequestModel.RefundRequest request)
        {
            try
            {
                var rs = await _refundRequestService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
