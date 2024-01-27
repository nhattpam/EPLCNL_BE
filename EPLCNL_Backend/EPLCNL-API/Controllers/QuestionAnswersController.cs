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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuestionAnswerResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuestionAnswerResponse>>> GetAll()
        {
            try
            {
                var rs = await _questionAnswerService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionAnswerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuestionAnswerResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _questionAnswerService.Get(id);
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
        public async Task<ActionResult<QuestionAnswerResponse>> Create([FromBody] QuestionAnswerRequest request)
        {
            try
            {
                var result = await _questionAnswerService.Create(request);
                return CreatedAtAction(nameof(Create), result);
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
