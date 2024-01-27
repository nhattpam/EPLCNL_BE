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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RefundResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundResponse>>> GetAll()
        {
            try
            {
                var rs = await _refundRequestService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefundResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RefundResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _refundRequestService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RefundResponse>> Create([FromBody] ViewModel.RequestModel.RefundRequest request)
        {
            try
            {
                var result = await _refundRequestService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
