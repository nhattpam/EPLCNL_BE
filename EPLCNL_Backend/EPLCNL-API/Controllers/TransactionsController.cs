using EPLCNL_API.VNPay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.TransactionsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing transactions.
    /// </summary>
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IVnPayService _vnPayService;

        public TransactionsController(ITransactionService transactionService, IVnPayService vnPayService)
        {
            _transactionService = transactionService;
            _vnPayService = vnPayService;
        }


        /// <summary>
        /// Get a list of all transactions.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TransactionResponse>>> GetAll()
        {
            try
            {
                var rs = await _transactionService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get transaction by transaction id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TransactionResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _transactionService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new transaction.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionResponse>> Create([FromBody] TransactionRequest request)
        {
            try
            {
                var result = await _transactionService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete transaction by transaction id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionResponse>> Delete(Guid id)
        {
            var rs = await _transactionService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update transaction by transaction id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionResponse>> Update(Guid id, [FromBody] TransactionRequest request)
        {
            try
            {
                var rs = await _transactionService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update transaction by transaction id.
        /// </summary>
        [HttpPost("/pay")]
        public async Task<ActionResult<string>> Pay()
        {
             var vnPayModel = new VnPaymentRequestModel
            {
                Amount= 1000,
                CreatedDate = DateTime.UtcNow,
                Description = "thanh toan don hang",
                FullName = "nhat",
                OrderId = new Random().Next(1000, 100000)
            };
            return _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
        }

    }
}
