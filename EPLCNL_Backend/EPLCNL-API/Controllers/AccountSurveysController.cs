using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AccountSurveysService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
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
        /// Get List Product In Menu
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<AccountSurveyResponse>>> GetAllAccountSurveys()
        {
            try
            {
                var rs = await _accountsurveyService.GetAccountSurveys();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountSurveyResponse>> Create([FromBody] AccountSurveyRequest request)
        {
            try
            {
                var result = await _accountsurveyService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<AccountSurveyResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _accountsurveyService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<AccountSurveyResponse>> Update([FromQuery] Guid id, [FromBody] AccountSurveyRequest request)
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
