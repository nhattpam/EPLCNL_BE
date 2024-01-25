using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.CentersService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/centers")]
    [ApiController]
    public class CentersController : ControllerBase
    {
        private readonly ICenterService _centerService;

        public CentersController(ICenterService centerService)
        {
            _centerService = centerService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CenterResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CenterResponse>>> GetAll()
        {
            try
            {
                var rs = await _centerService.GetAll();
                return CreatedAtAction(nameof(Create), rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CenterResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CenterResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _centerService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/tutors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CenterResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TutorResponse>>> GetAllTutorsByCenter(Guid id)
        {
            try
            {
                var rs = await _centerService.GetAllTutorsByCenter(id);
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
        public async Task<ActionResult<CenterResponse>> Create([FromBody] CenterRequest request)
        {
            try
            {
                var result = await _centerService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CenterResponse>> Delete( Guid id)
        {
            var rs = await _centerService.Delete(id);
            return Ok(rs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CenterResponse>> Update(Guid id, [FromBody] CenterRequest request)
        {
            try
            {
                var rs = await _centerService.Update(id, request);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
