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
        public async Task<ActionResult<List<WalletResponse>>> GetAllWallets()
        {
            try
            {
                var rs = await _walletService.GetWallets();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WalletResponse>> Create([FromBody] WalletRequest request)
        {
            try
            {
                var result = await _walletService.Create(request);
                return Ok(result);
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
