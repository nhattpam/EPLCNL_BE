using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AssignmentAttemptsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing assignment attempts.
    /// </summary>
    [Route("api/assignment-attempts")]
    [ApiController]
    public class AssignmentAttemptsController : ControllerBase
    {
        private readonly IAssignmentAttemptService _assignmentattemptService;

        public AssignmentAttemptsController(IAssignmentAttemptService assignmentattemptService)
        {
            _assignmentattemptService = assignmentattemptService;
        }

        /// <summary>
        /// Get a list of all assignment-attempts.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AssignmentAttemptResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AssignmentAttemptResponse>>> GetAll()
        {
            try
            {
                var rs = await _assignmentattemptService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get assignment-attempt by assignment-attempt id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentAttemptResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentAttemptResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _assignmentattemptService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Create new assignment-attempt.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AssignmentAttemptResponse>> Create([FromBody] AssignmentAttemptRequest request)
        {
            try
            {
                var result = await _assignmentattemptService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete assignment-attempt by assignment-attempt id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignmentAttemptResponse>> Delete(Guid id)
        {
            var rs = await _assignmentattemptService.Delete(id);
            return Ok(rs);
        }


        /// <summary>
        /// Update assignment-attempt by assignment-attempt id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentAttemptResponse>> Update(Guid id, [FromBody] AssignmentAttemptRequest request)
        {
            try
            {
                var rs = await _assignmentattemptService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of peer-reviews by assignment-attempt id.
        /// </summary>
        [HttpGet("{id}/peer-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PeerReviewResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PeerReviewResponse>>> GetAllPeerReviewsByAssignmentAttempt(Guid id)
        {
            try
            {
                var rs = await _assignmentattemptService.GetAllPeerReviewsByAssignmentAttempt(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
