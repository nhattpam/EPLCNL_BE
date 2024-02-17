using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ViolationsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/violations")]
    [ApiController]
    /// <summary>
    /// Controller for managing violations.
    /// </summary>
    public class ViolationsController : ControllerBase
    {
        private readonly IViolationService _violationService;

        public ViolationsController(IViolationService violationService)
        {
            _violationService = violationService;
        }

        /// <summary>
        /// Get a list of all violations.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ViolationResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ViolationResponse>>> GetAll()
        {
            try
            {
                var rs = await _violationService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get violation by violation id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViolationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ViolationResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _violationService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Create new violation.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ViolationResponse>> Create([FromBody] ViolationRequest request)
        {
            try
            {
                var result = await _violationService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete violation by violation id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViolationResponse>> Delete(Guid id)
        {
            var rs = await _violationService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update violation by violation id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ViolationResponse>> Update(Guid id, [FromBody] ViolationRequest request)
        {
            try
            {
                var rs = await _violationService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
    
}
