using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassPracticesService;
using Service.ClassPraticesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[class-practices]")]
    [ApiController]
    public class ClassPracticesController : ControllerBase
    {
        private readonly IClassPracticeService _classPracticeService;

        public ClassPracticesController(IClassPracticeService classPracticeService)
        {
            _classPracticeService = classPracticeService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClassPracticeResponse>>> GetAllClassPractices()
        {
            try
            {
                var rs = await _classPracticeService.GetClassPractices();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassPracticeResponse>> Create([FromBody] ClassPracticeRequest request)
        {
            try
            {
                var result = await _classPracticeService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassPracticeResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classPracticeService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassPracticeResponse>> Update([FromQuery] Guid id, [FromBody] ClassPracticeRequest request)
        {
            try
            {
                var rs = await _classPracticeService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
