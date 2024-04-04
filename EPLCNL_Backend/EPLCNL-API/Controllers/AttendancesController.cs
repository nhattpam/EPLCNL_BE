using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AttendancesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing attendances.
    /// </summary>
    [Route("api/attendances")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Get a list of all attendances.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AttendanceResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AttendanceResponse>>> GetAll()
        {
            try
            {
                var rs = await _attendanceService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get attendance by attendance id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AttendanceResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _attendanceService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new attendance.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AttendanceResponse>> Create([FromBody] AttendanceRequest request)
        {
            try
            {
                var result = await _attendanceService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete attendance by attendance id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttendanceResponse>> Delete(Guid id)
        {
            var rs = await _attendanceService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update attendance by attendance id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AttendanceResponse>> Update(Guid id, [FromBody] AttendanceRequest request)
        {
            try
            {
                var rs = await _attendanceService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get a list of learner-attendances by attendance id.
        /// </summary>
        [HttpGet("{id}/learner-attendances")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LearnerAttendanceResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LearnerAttendanceResponse>>> GetLearnerAttendanceByAttendance(Guid id)
        {

            try
            {
                var rs = await _attendanceService.GetLearnerAttendanceByAttendance(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
