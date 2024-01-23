using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LessonsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }


        [HttpGet]
        public async Task<ActionResult<List<LessonResponse>>> GetAllLessons()
        {
            try
            {
                var rs = await _lessonService.GetLessons();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonResponse>> Create([FromBody] LessonRequest request)
        {
            try
            {
                var result = await _lessonService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<LessonResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _lessonService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<LessonResponse>> Update([FromQuery] Guid id, [FromBody] LessonRequest request)
        {
            try
            {
                var rs = await _lessonService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
