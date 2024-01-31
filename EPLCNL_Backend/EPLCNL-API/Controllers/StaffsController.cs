using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.StaffsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing staffs.
    /// </summary>
    [Route("api/staffs")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService StaffService)
        {
            _staffService = StaffService;
        }

        /// <summary>
        /// Get a list of all staffs.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StaffResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<StaffResponse>>> GetAll()
        {
            try
            {
                var rs = await _staffService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get staff by staff id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StaffResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _staffService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of tutors by staff id.
        /// </summary>
        [HttpGet("{id}/tutors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TutorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TutorResponse>>> GetAllTutorsByStaff(Guid id)
        {
            try
            {
                var rs = await _staffService.GetAllTutorsByStaff(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of centers by staff id.
        /// </summary>
        [HttpGet("{id}/centers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CenterResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CenterResponse>>> GetAllCentersByStaff(Guid id)
        {
            try
            {
                var rs = await _staffService.GetAllCentersByStaff(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new staff.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StaffResponse>> Create([FromBody] StaffRequest request)
        {
            try
            {
                var result = await _staffService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete staff by staff id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffResponse>> Delete(Guid id)
        {
            var rs = await _staffService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update staff by staff id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<StaffResponse>> Update(Guid id, [FromBody] StaffRequest request)
        {
            try
            {
                var rs = await _staffService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
