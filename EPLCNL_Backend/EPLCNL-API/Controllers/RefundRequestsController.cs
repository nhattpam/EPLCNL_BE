using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CentersService;
using Service.RefundRequestsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing refund requests.
    /// </summary>
    [Route("api/refund-requests")]
    [ApiController]
    public class RefundRequestsController : ControllerBase
    {
        private readonly IRefundRequestService _refundRequestService;

        public RefundRequestsController(IRefundRequestService refundRequestService)
        {
            _refundRequestService = refundRequestService;
        }

        /// <summary>
        /// Get a list of all refund-requests.
        /// </summary>
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

        /// <summary>
        /// Get refund-request by refund-request id.
        /// </summary>
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

        /// <summary>
        /// Create new refund-request.
        /// </summary>
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

        /// <summary>
        /// Delete refund-request by refund-request id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefundResponse>> Delete(Guid id)
        {
            var rs = await _refundRequestService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update refund-request by refund-request id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RefundResponse>> Update(Guid id, [FromBody] ViewModel.RequestModel.RefundRequest request)
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

        /// <summary>
        /// Get a list of refund-histories by refund-request id.
        /// </summary>
        [HttpGet("{id}/refund-histories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RefundHistoryResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundHistoryResponse>>> GetRefundHistoryByRefundRequest(Guid id)
        {

            try
            {
                var rs = await _refundRequestService.GetRefundHistoryByRefundRequest(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of refund-surveys by refund-request id.
        /// </summary>
        [HttpGet("{id}/refund-surveys")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RefundSurveyResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundSurveyResponse>>> GetRefundSurveyByRefundRequest(Guid id)
        {

            try
            {
                var rs = await _refundRequestService.GetRefundSurveyByRefundRequest(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
