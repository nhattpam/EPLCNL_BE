using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuizAttemptsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing quiz attempts.
    /// </summary>
    [Route("api/quiz-attempts")]
    [ApiController]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizAttemptsController(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        // <summary>
        /// Get a list of all quiz-attempts.
        /// </summary>
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

        // <summary>
        /// Get quiz-attempt by quiz-attempt id.
        /// </summary>
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

        // <summary>
        /// Create new quiz-attempt.
        /// </summary>
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

        // <summary>
        /// Delete quiz-attempt by quiz-attempt id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizAttemptResponse>> Delete(Guid id)
        {
            var rs = await _quizAttemptService.Delete(id);
            return Ok(rs);
        }

        // <summary>
        /// Update quiz-attempt by quiz-attempt id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<QuizAttemptResponse>> Update(Guid id, [FromBody] QuizAttemptRequest request)
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
