using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.RefundSurveysService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing refund surveys.
    /// </summary>
    [Route("api/refund-surveys")]
    [ApiController]
    public class RefundSurveysController : ControllerBase
    {
        private readonly IRefundSurveyService _refundSurveyService;

        public RefundSurveysController(IRefundSurveyService refundSurveyService)
        {
            _refundSurveyService = refundSurveyService;
        }

        /// <summary>
        /// Get a list of all refund surveys.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RefundSurveyResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundSurveyResponse>>> GetAll()
        {
            try
            {
                var rs = await _refundSurveyService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get refundSurvey by refund survey id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefundSurveyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RefundSurveyResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _refundSurveyService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new refund survey.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RefundSurveyResponse>> Create([FromBody] RefundSurveyRequest request)
        {
            try
            {
                var result = await _refundSurveyService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete refund survey by refund survey id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefundSurveyResponse>> Delete(Guid id)
        {
            var rs = await _refundSurveyService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update refund survey by refund survey id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RefundSurveyResponse>> Update(Guid id, [FromBody] RefundSurveyRequest request)
        {
            try
            {
                var rs = await _refundSurveyService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
