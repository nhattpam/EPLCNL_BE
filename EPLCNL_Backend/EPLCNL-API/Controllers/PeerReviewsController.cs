using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AssignmentsService;
using Service.PeerReviewsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing peer-reviews.
    /// </summary>
    [Route("api/peer-reviews")]
    [ApiController]
    public class PeerReviewsController : ControllerBase
    {
        private readonly IPeerReviewService _peerReviewService;

        public PeerReviewsController(IPeerReviewService peerReviewService)
        {
            _peerReviewService = peerReviewService;
        }

        /// <summary>
        /// Get list of all peer-reviews.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PeerReviewResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PeerReviewResponse>>> GetAll()
        {
            try
            {
                var rs = await _peerReviewService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get peer-review by peer-review id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PeerReviewResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PeerReviewResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _peerReviewService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }



        /// <summary>
        /// Create new peer-review.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PeerReviewResponse>> Create([FromBody] PeerReviewRequest request)
        {
            try
            {
                var result = await _peerReviewService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete peer-review by peer-review id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PeerReviewResponse>> Delete(Guid id)
        {
            var rs = await _peerReviewService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update peer-review by peer-review id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PeerReviewResponse>> Update(Guid id, [FromBody] PeerReviewRequest request)
        {
            try
            {
                var rs = await _peerReviewService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


       
    }
}
