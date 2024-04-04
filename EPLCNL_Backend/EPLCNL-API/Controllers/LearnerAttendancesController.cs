using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LearnerAttendancesService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing learner attendances.
    /// </summary>
    [Route("api/learner-attendances")]
    [ApiController]
    public class LearnerAttendancesController : ControllerBase
    {
        private readonly ILearnerAttendanceService _learnerAttendanceService;

        public LearnerAttendancesController(ILearnerAttendanceService learnerAttendanceService)
        {
            _learnerAttendanceService = learnerAttendanceService;
        }

        /// <summary>
        /// Get a list of all learner-attendances.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LearnerAttendanceResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LearnerAttendanceResponse>>> GetAll()
        {
            try
            {
                var rs = await _learnerAttendanceService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get learnerAttendance by learner-attendance id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LearnerAttendanceResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LearnerAttendanceResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _learnerAttendanceService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new learner-attendance.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LearnerAttendanceResponse>> Create([FromBody] LearnerAttendanceRequest request)
        {
            try
            {
                var result = await _learnerAttendanceService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete learnerAttendance by learner-attendance id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LearnerAttendanceResponse>> Delete(Guid id)
        {
            var rs = await _learnerAttendanceService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update learnerAttendance by learner-attendance id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<LearnerAttendanceResponse>> Update(Guid id, [FromBody] LearnerAttendanceRequest request)
        {
            try
            {
                var rs = await _learnerAttendanceService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
