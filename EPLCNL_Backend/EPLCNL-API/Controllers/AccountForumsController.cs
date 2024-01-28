using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AccountForumsService;
using System.Net.Mime;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/account-forums")]
    [ApiController]
    public class AccountForumsController : ControllerBase
    {
        private readonly IAccountForumService _accountforumService;

        public AccountForumsController(IAccountForumService accountforumService)
        {
            _accountforumService = accountforumService;
        }

 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountForumResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AccountForumResponse>>> GetAll()
        {
            try
            {
                var rs = await _accountforumService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountForumResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountForumResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _accountforumService.Get(id);
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
        public async Task<ActionResult<AccountForumResponse>> Create([FromBody] AccountForumRequest request)
        {
            try
            {
                var result = await _accountforumService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountForumResponse>> Delete(Guid id)
        {
            var rs = await _accountforumService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<AccountForumResponse>> Update(Guid id, [FromBody] AccountForumRequest request)
        {
            try
            {
                var rs = await _accountforumService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
