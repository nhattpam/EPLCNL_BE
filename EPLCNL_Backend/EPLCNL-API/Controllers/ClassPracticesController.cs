using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassPracticesService;
using Service.ClassPraticesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing class practices.
    /// </summary>
    [Route("api/class-practices")]
    [ApiController]
    public class ClassPracticesController : ControllerBase
    {
        private readonly IClassPracticeService _classPracticeService;

        public ClassPracticesController(IClassPracticeService classPracticeService)
        {
            _classPracticeService = classPracticeService;
        }

        /// <summary>
        /// Get a list of all class-practices.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassPracticeResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassPracticeResponse>>> GetAll()
        {
            try
            {
                var rs = await _classPracticeService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get class-practice by class-practice id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassPracticeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClassPracticeResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _classPracticeService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new class-practice.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassPracticeResponse>> Create([FromBody] ClassPracticeRequest request)
        {
            try
            {
                var result = await _classPracticeService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete class-practice by class-practice id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassPracticeResponse>> Delete(Guid id)
        {
            var rs = await _classPracticeService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update class-practice by class-practice id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClassPracticeResponse>> Update(Guid id, [FromBody] ClassPracticeRequest request)
        {
            try
            {
                var rs = await _classPracticeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
