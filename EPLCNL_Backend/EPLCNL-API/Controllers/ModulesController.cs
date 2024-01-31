using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ModulesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing modules.
    /// </summary>
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// Get a list of all modules.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModuleResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ModuleResponse>>> GetAll()
        {
            try
            {
                var rs = await _moduleService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get module by module id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ModuleResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _moduleService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of assignments by module id.
        /// </summary>
        [HttpGet("{id}/assignments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AssignmentResponse>>> GetAllAssignmentsByModule(Guid id)
        {
            try
            {
                var rs = await _moduleService.GetAllAssignmentsByModule(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of lessons by module id.
        /// </summary>
        [HttpGet("{id}/lessons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LessonResponse>>> GetAllLessonsByModule(Guid id)
        {
            try
            {
                var rs = await _moduleService.GetAllLessonsByModule(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Get a list of quizzes by module id.
        /// </summary>
        [HttpGet("{id}/quizzes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuizResponse>>> GetAllQuizzesByModule(Guid id)
        {
            try
            {
                var rs = await _moduleService.GetAllQuizzesByModule(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Create new module.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuleResponse>> Create([FromBody] ModuleRequest request)
        {
            try
            {
                var result = await _moduleService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete module by module id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ModuleResponse>> Delete(Guid id)
        {
            var rs = await _moduleService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update module by module id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ModuleResponse>> Update(Guid id, [FromBody] ModuleRequest request)
        {
            try
            {
                var rs = await _moduleService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
