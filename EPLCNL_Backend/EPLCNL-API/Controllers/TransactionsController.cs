using EPLCNL_API.VNPay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.AccountsService;
using Service.LearnersService;
using Service.TransactionsService;
using System.Net.Mail;
using System.Net.Mime;
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
        private readonly ILearnerService _learnerService;
        private readonly IAccountService _accountService;

        public TransactionsController(ITransactionService transactionService, IVnPayService vnPayService
            , ILearnerService learnerService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _vnPayService = vnPayService;
            _learnerService = learnerService;
            _accountService = accountService;
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
        public async Task<ActionResult<List<TransactionResponse>>> Get(Guid id)
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
        /// Pay through VnPay by transaction id.
        /// </summary>
        [HttpPost("{id}/pay")]
        public async Task<ActionResult<string>> Pay(Guid id)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;

            var transaction = await _transactionService.Get(id);
            if(transaction == null)
            {
                return NotFound();
            }
            else
            {
                var learner = await _learnerService.Get(transaction.LearnerId);
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = transaction.Amount,
                    CreatedDate = localTime,
                    Description = "Payment-For-Order:",
                    FullName = learner.Account.FullName,
                    OrderId = id
                };
                return _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
            }
           
        }


        /// <summary>
        /// Pay through Wallet by transaction id.
        /// </summary>
        [HttpPost("{id}/wallet-payment")]
        public async Task<ActionResult<bool>> PayByWallet(Guid id)
        {
          var response = await _transactionService.PayByWallet(id);
            if (response == true)
            {
                var transaction = await _transactionService.Get(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                else
                {
                    var updateTransaction = new TransactionRequest
                    {
                        Amount = transaction.Amount,
                        PaymentMethodId = transaction.PaymentMethodId,
                        Status = "DONE",
                        TransactionDate = transaction.TransactionDate,
                        LearnerId = transaction.LearnerId,  
                        CourseId = transaction.CourseId,
                    };

                    await _transactionService.Update(transaction.Id, updateTransaction);
                }
                
            }
            return response;
        }


    }
}
