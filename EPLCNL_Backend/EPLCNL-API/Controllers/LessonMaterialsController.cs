using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LessonMaterialsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/lesson-materials")]
    [ApiController]
    public class LessonMaterialsController : ControllerBase
    {
        private readonly ILessonMaterialService _lessonMaterialService;

        public LessonMaterialsController(ILessonMaterialService lessonMaterialService)
        {
            _lessonMaterialService = lessonMaterialService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonMaterialResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LessonMaterialResponse>>> GetAll()
        {
            try
            {
                var rs = await _lessonMaterialService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LessonMaterialResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LessonMaterialResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _lessonMaterialService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LessonMaterialResponse>> Create([FromBody] LessonMaterialRequest request)
        {
            try
            {
                var result = await _lessonMaterialService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LessonMaterialResponse>> Delete(Guid id)
        {
            var rs = await _lessonMaterialService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LessonMaterialResponse>> Update(Guid id, [FromBody] LessonMaterialRequest request)
        {
            try
            {
                var rs = await _lessonMaterialService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
