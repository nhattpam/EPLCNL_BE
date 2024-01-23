using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LearnersService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/learners")]
    [ApiController]
    public class LearnersController : ControllerBase
    {
        private readonly ILearnerService _learnerService;

        public LearnersController(ILearnerService learnerService)
        {
            _learnerService = learnerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<LearnerResponse>>> GetAllLearners()
        {
            try
            {
                var rs = await _learnerService.GetLearners();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LearnerResponse>> Create([FromBody] LearnerRequest request)
        {
            try
            {
                var result = await _learnerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<LearnerResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _learnerService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<LearnerResponse>> Update([FromQuery] Guid id, [FromBody] LearnerRequest request)
        {
            try
            {
                var rs = await _learnerService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
