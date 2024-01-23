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
        public async Task<ActionResult<List<ClassLessonResponse>>> GetAllClassLessons()
        {
            try
            {
                var rs = await _classLessonService.GetClassLessons();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassLessonResponse>> Create([FromBody] ClassLessonRequest request)
        {
            try
            {
                var result = await _classLessonService.Create(request);
                return Ok(result);
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
