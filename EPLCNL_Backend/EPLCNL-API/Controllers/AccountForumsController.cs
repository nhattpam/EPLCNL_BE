using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AccountForumsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/accountforums")]
    [ApiController]
    public class AccountForumsController : ControllerBase
    {
        private readonly IAccountForumService _accountforumService;

        public AccountForumsController(IAccountForumService accountforumService)
        {
            _accountforumService = accountforumService;
        }

        /// <summary>
        /// Get List Product In Menu
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<AccountForumResponse>>> GetAllAccountForums()
        {
            try
            {
                var rs = await _accountforumService.GetAccountForums();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountForumResponse>> Create([FromBody] AccountForumRequest request)
        {
            try
            {
                var result = await _accountforumService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<AccountForumResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _accountforumService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<AccountForumResponse>> Update([FromQuery] Guid id, [FromBody] AccountForumRequest request)
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
