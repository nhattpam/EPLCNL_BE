using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ClassTopicsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/class-topics")]
    [ApiController]
    public class ClassTopicsController : ControllerBase
    {
        private readonly IClassTopicService _classTopicService;

        public ClassTopicsController(IClassTopicService classTopicService)
        {
            _classTopicService = classTopicService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassTopicResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ClassTopicResponse>>> GetAll()
        {
            try
            {
                var rs = await _classTopicService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassTopicResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClassTopicResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _classTopicService.Get(id);
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
        public async Task<ActionResult<ClassTopicResponse>> Create([FromBody] ClassTopicRequest request)
        {
            try
            {
                var result = await _classTopicService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassTopicResponse>> Delete(Guid id)
        {
            var rs = await _classTopicService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ClassTopicResponse>> Update(Guid id, [FromBody] ClassTopicRequest request)
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
