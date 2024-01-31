using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaperWorkTypesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing paper work types.
    /// </summary>
    [Route("api/paper-work-types")]
    [ApiController]
    public class PaperWorkTypesController : ControllerBase
    {
        private readonly IPaperWorkTypeService _paperWorkTypeService;

        public PaperWorkTypesController(IPaperWorkTypeService paperWorkTypeService)
        {
            _paperWorkTypeService = paperWorkTypeService;
        }

        /// <summary>
        /// Get a list of all paper-work-types.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaperWorkTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PaperWorkTypeResponse>>> GetAll()
        {
            try
            {
                var rs = await _paperWorkTypeService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get paper-work-type by paper-work-type id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaperWorkTypeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaperWorkTypeResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _paperWorkTypeService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new paper-work-type.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaperWorkTypeResponse>> Create([FromBody] PaperWorkTypeRequest request)
        {
            try
            {
                var result = await _paperWorkTypeService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete paper-work-type by paper-work-type id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaperWorkTypeResponse>> Delete( Guid id)
        {
            var rs = await _paperWorkTypeService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update paper-work-type by paper-work-type id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PaperWorkTypeResponse>> Update(Guid id, [FromBody] PaperWorkTypeRequest request)
        {
            try
            {
                var rs = await _paperWorkTypeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
