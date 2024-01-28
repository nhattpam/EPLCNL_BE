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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ForumResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ForumResponse>>> GetAll()
        {
            try
            {
                var rs = await _forumService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForumResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ForumResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _forumService.Get(id);
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
        public async Task<ActionResult<ForumResponse>> Create([FromBody] ForumRequest request)
        {
            try
            {
                var result = await _forumService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ForumResponse>> Delete(Guid id)
        {
            var rs = await _forumService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ForumResponse>> Update(Guid id, [FromBody] ForumRequest request)
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
