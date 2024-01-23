using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FeedbacksService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }


        [HttpGet]
        public async Task<ActionResult<List<FeedbackResponse>>> GetAllFeedbacks()
        {
            try
            {
                var rs = await _feedbackService.GetFeedbacks();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackResponse>> Create([FromBody] FeedbackRequest request)
        {
            try
            {
                var result = await _feedbackService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<FeedbackResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _feedbackService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<FeedbackResponse>> Update([FromQuery] Guid id, [FromBody] FeedbackRequest request)
        {
            try
            {
                var rs = await _feedbackService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
