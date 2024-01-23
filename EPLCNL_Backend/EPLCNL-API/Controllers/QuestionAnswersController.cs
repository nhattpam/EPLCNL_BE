using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuestionAnswersService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/question-answers")]
    [ApiController]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IQuestionAnswerService _questionAnswerService;

        public QuestionAnswersController(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<QuestionAnswerResponse>>> GetAllQuestionAnswers()
        {
            try
            {
                var rs = await _questionAnswerService.GetQuestionAnswers();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionAnswerResponse>> Create([FromBody] QuestionAnswerRequest request)
        {
            try
            {
                var result = await _questionAnswerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<QuestionAnswerResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _questionAnswerService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<QuestionAnswerResponse>> Update([FromQuery] Guid id, [FromBody] QuestionAnswerRequest request)
        {
            try
            {
                var rs = await _questionAnswerService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
