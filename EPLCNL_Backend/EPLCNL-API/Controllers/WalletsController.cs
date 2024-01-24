using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.WalletsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WalletResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<WalletResponse>>> GetAll()
        {
            try
            {
                var rs = await _walletService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WalletResponse>> Create([FromBody] WalletRequest request)
        {
            try
            {
                var result = await _walletService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<WalletResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _walletService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<WalletResponse>> Update([FromQuery] Guid id, [FromBody] WalletRequest request)
        {
            try
            {
                var rs = await _walletService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
