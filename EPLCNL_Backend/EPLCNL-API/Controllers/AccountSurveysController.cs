using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AccountSurveysService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing account surveys.
    /// </summary>
    [Route("api/account-surveys")]
    [ApiController]
    public class AccountSurveysController : ControllerBase
    {
        private readonly IAccountSurveyService _accountsurveyService;

        public AccountSurveysController(IAccountSurveyService accountsurveyService)
        {
            _accountsurveyService = accountsurveyService;
        }
        /// <summary>
        /// Get a list of all account-surveys.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountSurveyResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AccountSurveyResponse>>> GetAll()
        {
            try
            {
                var rs = await _accountsurveyService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get account-survey by account-survey id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountSurveyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountSurveyResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _accountsurveyService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Create new account-survey.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountSurveyResponse>> Create([FromBody] AccountSurveyRequest request)
        {
            try
            {
                var result = await _accountsurveyService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete account-survey by account-survey id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountSurveyResponse>> Delete(Guid id)
        {
            var rs = await _accountsurveyService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update account-survey by account-survey id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountSurveyResponse>> Update(Guid id, [FromBody] AccountSurveyRequest request)
        {
            try
            {
                var rs = await _accountsurveyService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
