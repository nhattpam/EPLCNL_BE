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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AccountResponse>>> GetAll()
        {
            try
            {
                var rs = await _accountService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _accountService.Get(id);
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
        public async Task<ActionResult<AccountResponse>> Create([FromBody] AccountRequest request)
        {
            try
            {
                var result = await _accountService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
