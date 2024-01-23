using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.QuizzesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }


        [HttpGet]
        public async Task<ActionResult<List<QuizResponse>>> GetAllQuizs()
        {
            try
            {
                var rs = await _quizService.GetQuizzes();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuizResponse>> Create([FromBody] QuizRequest request)
        {
            try
            {
                var result = await _quizService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<QuizResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _quizService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<QuizResponse>> Update([FromQuery] Guid id, [FromBody] QuizRequest request)
        {
            try
            {
                var rs = await _quizService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
