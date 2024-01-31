using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaperWorksService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing paper works.
    /// </summary>
    [Route("api/paper-works")]
    [ApiController]
    public class PaperWorksController : ControllerBase
    {
        private readonly IPaperWorkService _paperWorkService;

        public PaperWorksController(IPaperWorkService paperWorkService)
        {
            _paperWorkService = paperWorkService;
        }

        /// <summary>
        /// Get a list of all paper-works.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaperWorkResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PaperWorkResponse>>> GetAll()
        {
            try
            {
                var rs = await _paperWorkService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get paper-work by paper-work id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaperWorkResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaperWorkResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _paperWorkService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new paper-work.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaperWorkResponse>> Create([FromBody] PaperWorkRequest request)
        {
            try
            {
                var result = await _paperWorkService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete paper-work by paper-work id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaperWorkResponse>> Delete(Guid id)
        {
            var rs = await _paperWorkService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update paper-work by paper-work id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PaperWorkResponse>> Update(Guid id, [FromBody] PaperWorkRequest request)
        {
            try
            {
                var rs = await _paperWorkService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
