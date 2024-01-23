using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuizAttemptsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/quiz-attempts")]
    [ApiController]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizAttemptsController(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }


        [HttpGet]
        public async Task<ActionResult<List<QuizAttemptResponse>>> GetAllQuizAttempts()
        {
            try
            {
                var rs = await _quizAttemptService.GetQuizAttempts();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuizAttemptResponse>> Create([FromBody] QuizAttemptRequest request)
        {
            try
            {
                var result = await _quizAttemptService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<QuizAttemptResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _quizAttemptService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<QuizAttemptResponse>> Update([FromQuery] Guid id, [FromBody] QuizAttemptRequest request)
        {
            try
            {
                var rs = await _quizAttemptService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
