using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SalariesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing salaries.
    /// </summary>
    [Route("api/salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalariesController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }
        /// <summary>
        /// Get a list of all salaries.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SalaryResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<SalaryResponse>>> GetAll()
        {
            try
            {
                var rs = await _salaryService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get salary by salary id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalaryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SalaryResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _salaryService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Create new salary.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SalaryResponse>> Create([FromBody] SalaryRequest request)
        {
            try
            {
                var result = await _salaryService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete salary by salary id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalaryResponse>> Delete(Guid id)
        {
            var rs = await _salaryService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update salary by salary id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<SalaryResponse>> Update(Guid id, [FromBody] SalaryRequest request)
        {
            try
            {
                var rs = await _salaryService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
