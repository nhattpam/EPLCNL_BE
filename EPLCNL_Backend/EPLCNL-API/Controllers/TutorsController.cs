using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.TutorService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/tutors")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutorsController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }


        [HttpGet]
        public async Task<ActionResult<List<TutorResponse>>> GetAllTutors()
        {
            try
            {
                var rs = await _tutorService.GetTutors();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TutorResponse>> Create([FromBody] TutorRequest request)
        {
            try
            {
                var result = await _tutorService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<TutorResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _tutorService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<TutorResponse>> Update([FromQuery] Guid id, [FromBody] TutorRequest request)
        {
            try
            {
                var rs = await _tutorService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
