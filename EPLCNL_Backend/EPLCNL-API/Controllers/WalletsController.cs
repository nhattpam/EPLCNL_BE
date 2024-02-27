using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AccountsService;
using Service.WalletsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing wallets.
    /// </summary>
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IAccountService _accountService;

        public WalletsController(IWalletService walletService, IAccountService accountService)
        {
            _walletService = walletService;
            _accountService = accountService;
        }

        /// <summary>
        /// Get a list of all wallets.
        /// </summary>
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

        /// <summary>
        /// Get wallet by wallet id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WalletResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WalletResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _walletService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
       

        /// <summary>
        /// Create new wallet.
        /// </summary>
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

        /// <summary>
        /// Delete wallet by wallet id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<WalletResponse>> Delete(Guid id)
        {
            var rs = await _walletService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update wallet by wallet id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<WalletResponse>> Update(Guid id, [FromBody] WalletRequest request)
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

        ///// <summary>
        ///// Transfer money through VnPay by wallet of accountId.
        ///// </summary>
        //[HttpPost("{id}/pay")]
        //public async Task<ActionResult<string>> Pay(Guid id)
        //{
        //    // Set the UTC offset for UTC+7
        //    TimeSpan utcOffset = TimeSpan.FromHours(7);

        //    // Get the current UTC time
        //    DateTime utcNow = DateTime.UtcNow;

        //    // Convert the UTC time to UTC+7
        //    DateTime localTime = utcNow + utcOffset;

        //    var accountWallet = await _accountService.GetWalletByAcount(id);
        //    if (accountWallet == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        var vnPayModel = new VnPaymentRequestModel
        //        {
        //            Amount = accountWallet.Amount,
        //            CreatedDate = localTime,
        //            Description = "Payment-For-Order:",
        //            FullName = learner.Account.FullName,
        //            OrderId = id
        //        };
        //        return _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
        //    }

        //}
    }
}
