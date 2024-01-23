using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.TransactionsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransactions()
        {
            try
            {
                var rs = await _transactionService.GetTransactions();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> Create([FromBody] TransactionRequest request)
        {
            try
            {
                var result = await _transactionService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<TransactionResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _transactionService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<TransactionResponse>> Update([FromQuery] Guid id, [FromBody] TransactionRequest request)
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
    }
}
