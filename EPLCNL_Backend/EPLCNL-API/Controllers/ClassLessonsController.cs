using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.ClassLessonsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/class-lessons")]
    [ApiController]
    public class ClassLessonsController : ControllerBase
    {
        private readonly IClassLessonService _classLessonService;

        public ClassLessonsController(IClassLessonService classLessonService)
        {
            _classLessonService = classLessonService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassLessonResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassLessonResponse>>> GetAll()
        {
            try
            {
                var rs = await _classLessonService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassLessonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClassLessonResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _classLessonService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/class-topics")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassTopicResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassTopicResponse>>> GetAllClassTopicsByClassLesson(Guid id)
        {
            try
            {
                var rs = await _classLessonService.GetAllClassTopicsByClassLesson(id);
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
        public async Task<ActionResult<ClassLessonResponse>> Create([FromBody] ClassLessonRequest request)
        {
            try
            {
                var result = await _classLessonService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassLessonResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classLessonService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassLessonResponse>> Update([FromQuery] Guid id, [FromBody] ClassLessonRequest request)
        {
            try
            {
                var rs = await _classLessonService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
