using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuestionsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpGet]
        public async Task<ActionResult<List<QuestionResponse>>> GetAllQuestions()
        {
            try
            {
                var rs = await _questionService.GetQuestions();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionResponse>> Create([FromBody] QuestionRequest request)
        {
            try
            {
                var result = await _questionService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<QuestionResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _questionService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<QuestionResponse>> Update([FromQuery] Guid id, [FromBody] QuestionRequest request)
        {
            try
            {
                var rs = await _questionService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
