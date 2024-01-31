using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassModulesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing class modules.
    /// </summary>
    [Route("api/class-modules")]
    [ApiController]
    public class ClassModulesController : ControllerBase
    {
        private readonly IClassModuleService _classModuleService;

        public ClassModulesController(IClassModuleService classModuleService)
        {
            _classModuleService = classModuleService;
        }

        /// <summary>
        /// Get a list of all class-modules.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassModuleResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassModuleResponse>>> GetAll()
        {
            try
            {
                var rs = await _classModuleService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get class-module by class-module id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassModuleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClassModuleResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _classModuleService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new class-module.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassModuleResponse>> Create([FromBody] ClassModuleRequest request)
        {
            try
            {
                var result = await _classModuleService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete class-module by class-module id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassModuleResponse>> Delete(Guid id)
        {
            var rs = await _classModuleService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update class-module by class-module id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClassModuleResponse>> Update(Guid id, [FromBody] ClassModuleRequest request)
        {
            try
            {
                var rs = await _classModuleService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
