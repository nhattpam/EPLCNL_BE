using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ForumsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/forums")]
    [ApiController]
    public class ForumsController : ControllerBase
    {
        private readonly IForumService _forumService;

        public ForumsController(IForumService forumService)
        {
            _forumService = forumService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ForumResponse>>> GetAllForums()
        {
            try
            {
                var rs = await _forumService.GetForums();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ForumResponse>> Create([FromBody] ForumRequest request)
        {
            try
            {
                var result = await _forumService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ForumResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _forumService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<ForumResponse>> Update([FromQuery] Guid id, [FromBody] ForumRequest request)
        {
            try
            {
                var rs = await _forumService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
