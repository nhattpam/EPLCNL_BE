using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EnrollmentsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _centerService;

        public EnrollmentsController(IEnrollmentService centerService)
        {
            _centerService = centerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<EnrollmentResponse>>> GetAllEnrollments()
        {
            try
            {
                var rs = await _centerService.GetEnrollments();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentResponse>> Create([FromBody] EnrollmentRequest request)
        {
            try
            {
                var result = await _centerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<EnrollmentResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _centerService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<EnrollmentResponse>> Update([FromQuery] Guid id, [FromBody] EnrollmentRequest request)
        {
            try
            {
                var rs = await _centerService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
