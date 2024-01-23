using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.SurveysService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpGet]
        public async Task<ActionResult<List<SurveyResponse>>> GetAllSurveys()
        {
            try
            {
                var rs = await _surveyService.GetSurveys();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponse>> Create([FromBody] SurveyRequest request)
        {
            try
            {
                var result = await _surveyService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<SurveyResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _surveyService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<SurveyResponse>> Update([FromQuery] Guid id, [FromBody] SurveyRequest request)
        {
            try
            {
                var rs = await _surveyService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
