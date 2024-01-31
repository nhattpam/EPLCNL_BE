using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.ClassLessonsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing class lessons.
    /// </summary>
    [Route("api/class-lessons")]
    [ApiController]
    public class ClassLessonsController : ControllerBase
    {
        private readonly IClassLessonService _classLessonService;

        public ClassLessonsController(IClassLessonService classLessonService)
        {
            _classLessonService = classLessonService;
        }

        /// <summary>
        /// Get a list of all class-lessons.
        /// </summary>
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

        /// <summary>
        /// Get class-lesson by class-lesson id.
        /// </summary>
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

        /// <summary>
        /// Get a list of class-topics by class-lesson id.
        /// </summary>
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

        /// <summary>
        /// Create new class-lesson.
        /// </summary>
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

        /// <summary>
        /// Delete class-lesson by class-lesson id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassLessonResponse>> Delete(Guid id)
        {
            var rs = await _classLessonService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update class-lesson by class-lesson id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClassLessonResponse>> Update(Guid id, [FromBody] ClassLessonRequest request)
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
