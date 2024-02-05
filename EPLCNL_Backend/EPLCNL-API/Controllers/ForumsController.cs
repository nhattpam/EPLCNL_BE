using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ForumsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing forums.
    /// </summary>
    [Route("api/forums")]
    [ApiController]
    public class ForumsController : ControllerBase
    {
        private readonly IForumService _forumService;

        public ForumsController(IForumService forumService)
        {
            _forumService = forumService;
        }

        /// <summary>
        /// Get a list of all forums.
        /// </summary>
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

        /// <summary>
        /// Get forum by forum id.
        /// </summary>
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

        /// <summary>
        /// Create new forum.
        /// </summary>
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

        /// <summary>
        /// Delete forum by forum id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ForumResponse>> Delete(Guid id)
        {
            var rs = await _forumService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update forum by forum id.
        /// </summary>
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

        /// <summary>
        /// Get a list of account forums by forum id.
        /// </summary>
        [HttpGet("{id}/account-forums")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountForumResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AccountForumResponse>>> GetAllAccountForumsByForum(Guid id)
        {
            try
            {
                var rs = await _forumService.GetAllAccountForumsByForum(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
