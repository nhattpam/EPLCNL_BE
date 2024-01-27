using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.StaffsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/Staffs")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _StaffService;

        public StaffsController(IStaffService StaffService)
        {
            _StaffService = StaffService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StaffResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<StaffResponse>>> GetAll()
        {
            try
            {
                var rs = await _StaffService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StaffResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _StaffService.Get(id);
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
        public async Task<ActionResult<StaffResponse>> Create([FromBody] StaffRequest request)
        {
            try
            {
                var result = await _StaffService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<StaffResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _StaffService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<StaffResponse>> Update([FromQuery] Guid id, [FromBody] StaffRequest request)
        {
            try
            {
                var rs = await _StaffService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
