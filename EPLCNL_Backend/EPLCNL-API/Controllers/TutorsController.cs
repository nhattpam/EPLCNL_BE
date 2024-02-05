using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.TutorService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing tutors.
    /// </summary>
    [Route("api/tutors")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutorsController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }


        /// <summary>
        /// Get a list of all tutors.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TutorResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TutorResponse>>> GetAll()
        {
            try
            {
                var rs = await _tutorService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get tutor by tutor id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TutorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TutorResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _tutorService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of courses by tutor id.
        /// </summary>
        [HttpGet("{id}/courses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CourseResponse>>> GetAllCoursesByTutor(Guid id)
        {
            try
            {
                var rs = await _tutorService.GetAllCoursesByTutor(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of paper-works by tutor id.
        /// </summary>
        [HttpGet("{id}/paper-works")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaperWorkResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PaperWorkResponse>>> GetAllPaperWorksByTutor(Guid id)
        {
            try
            {
                var rs = await _tutorService.GetAllPaperWorksByTutor(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new tutor.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TutorResponse>> Create([FromBody] TutorRequest request)
        {
            try
            {
                var result = await _tutorService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete tutor by tutor id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TutorResponse>> Delete(Guid id)
        {
            var rs = await _tutorService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update tutor by tutor id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TutorResponse>> Update(Guid id, [FromBody] TutorRequest request)
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

        
        /// <summary>
        /// Get a list of forums by tutor id.
        /// </summary>
        [HttpGet("{id}/forums")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForumResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ForumResponse>>> GetAllForumsByTutor(Guid id)
        {
            try
            {
                var rs = await _tutorService.GetAllForumsByTutor(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
