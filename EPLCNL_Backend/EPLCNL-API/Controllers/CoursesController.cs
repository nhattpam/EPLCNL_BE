using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CoursesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[courses]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CourseResponse>>> GetAllCourses()
        {
            try
            {
                var rs = await _courseService.GetCourses();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CourseResponse>> Create([FromBody] CourseRequest request)
        {
            try
            {
                var result = await _courseService.Create(request);
                return Ok(result);
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
