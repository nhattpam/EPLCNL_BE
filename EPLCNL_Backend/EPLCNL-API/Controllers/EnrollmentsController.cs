using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EnrollmentsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing enrollments.
    /// </summary>
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// Get a list of all enrollments.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EnrollmentResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<EnrollmentResponse>>> GetAll()
        {
            try
            {
                var rs = await _enrollmentService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get enrollment by enrollment id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EnrollmentResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _enrollmentService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new enrollment.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnrollmentResponse>> Create([FromBody] EnrollmentRequest request)
        {
            try
            {
                var result = await _enrollmentService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete enrollment by enrollment id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EnrollmentResponse>> Delete(Guid id)
        {
            var rs = await _enrollmentService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update enrollment by enrollment id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentResponse>> Update(Guid id, [FromBody] EnrollmentRequest request)
        {
            try
            {
                var rs = await _enrollmentService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get enrollment by learner and course id.
        /// </summary>
        [HttpGet("learners/{learnerId}/courses/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EnrollmentResponse>> GetEnrollmentByLearnerAndCourseId(Guid learnerId, Guid courseId)
        {
            try
            {
                var rs = await _enrollmentService.GetEnrollmentByLearnerAndCourseId(learnerId, courseId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete enrollment by learner and course id.
        /// </summary>
        [HttpDelete("learners/{learnerId}/courses/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EnrollmentResponse>> DeleteEnrollmentByLearnerAndCourseId(Guid learnerId, Guid courseId)
        {
            try
            {
                var rs = await _enrollmentService.DeleteEnrollmentByLearnerAndCourseId(learnerId, courseId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
