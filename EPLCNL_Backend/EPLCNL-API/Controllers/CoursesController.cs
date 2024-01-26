using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CoursesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CourseResponse>>> GetAll()
        {
            try
            {
                var rs = await _courseService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CourseResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _courseService.Get(id);
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
        public async Task<ActionResult<CourseResponse>> Create([FromBody] CourseRequest request)
        {
            try
            {
                var result = await _courseService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<CourseResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _courseService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<CourseResponse>> Update([FromQuery] Guid id, [FromBody] CourseRequest request)
        {
            try
            {
                var rs = await _courseService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
