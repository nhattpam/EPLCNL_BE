using Microsoft.AspNetCore.Mvc;
using Service.RefundHistoriesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/refund-histories")]
    [ApiController]
    /// <summary>
    /// Controller for managing refund-histories.
    /// </summary>
    public class RefundHistoriesController : ControllerBase
    {
        private readonly IRefundHistoryService _refundHistoryService;

        public RefundHistoriesController(IRefundHistoryService refundHistoryService)
        {
            _refundHistoryService = refundHistoryService;
        }

        /// <summary>
        /// Get a list of all refund-histories.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RefundHistoryResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundHistoryResponse>>> GetAll()
        {
            try
            {
                var rs = await _refundHistoryService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get refund History by refund-history id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefundHistoryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RefundHistoryResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _refundHistoryService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Create new refund-history.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RefundHistoryResponse>> Create([FromBody] RefundHistoryRequest request)
        {
            try
            {
                var result = await _refundHistoryService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete refund history by refund-history id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefundHistoryResponse>> Delete(Guid id)
        {
            var rs = await _refundHistoryService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update refund-history by refund-history id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RefundHistoryResponse>> Update(Guid id, [FromBody] RefundHistoryRequest request)
        {
            try
            {
                var rs = await _refundHistoryService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
