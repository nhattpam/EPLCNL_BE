using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassTopicsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[class-topics]")]
    [ApiController]
    public class ClassTopicsController : ControllerBase
    {
        private readonly IClassTopicService _classTopicService;

        public ClassTopicsController(IClassTopicService classTopicService)
        {
            _classTopicService = classTopicService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClassTopicResponse>>> GetAllClassTopics()
        {
            try
            {
                var rs = await _classTopicService.GetClassTopics();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassTopicResponse>> Create([FromBody] ClassTopicRequest request)
        {
            try
            {
                var result = await _classTopicService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ClassTopicResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _classTopicService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ClassTopicResponse>> Update([FromQuery] Guid id, [FromBody] ClassTopicRequest request)
        {
            try
            {
                var rs = await _classTopicService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
