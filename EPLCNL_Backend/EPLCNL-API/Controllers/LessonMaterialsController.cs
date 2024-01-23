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
        public async Task<ActionResult<List<LessonMaterialResponse>>> GetAllLessonMaterials()
        {
            try
            {
                var rs = await _lessonMaterialService.GetLessonMaterials();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonMaterialResponse>> Create([FromBody] LessonMaterialRequest request)
        {
            try
            {
                var result = await _lessonMaterialService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<LessonMaterialResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _lessonMaterialService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<LessonMaterialResponse>> Update([FromQuery] Guid id, [FromBody] LessonMaterialRequest request)
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
