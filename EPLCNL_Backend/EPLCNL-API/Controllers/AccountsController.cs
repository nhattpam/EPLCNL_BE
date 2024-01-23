using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AccountsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult<List<AccountResponse>>> GetAllAccounts()
        {
            try
            {
                var rs = await _accountService.GetAccounts();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<AccountResponse>> Create([FromBody] AccountRequest request)
        {
            try
            {
                var result = await _accountService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<AccountResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _accountService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<AccountResponse>> Update([FromQuery] Guid id, [FromBody] AccountRequest request)
        {
            try
            {
                var rs = await _accountService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
