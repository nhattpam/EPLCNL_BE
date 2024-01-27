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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizAttemptResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuizAttemptResponse>>> GetAll()
        {
            try
            {
                var rs = await _quizAttemptService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizAttemptResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuizAttemptResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _quizAttemptService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuizAttemptResponse>> Create([FromBody] QuizAttemptRequest request)
        {
            try
            {
                var result = await _quizAttemptService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
