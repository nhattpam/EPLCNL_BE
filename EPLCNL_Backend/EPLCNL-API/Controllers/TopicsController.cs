using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.TopicsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing topics.
    /// </summary>
    [Route("api/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _classTopicService;

        public TopicsController(ITopicService classTopicService)
        {
            _classTopicService = classTopicService;
        }

        /// <summary>
        /// Get a list of all topics.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TopicResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TopicResponse>>> GetAll()
        {
            try
            {
                var rs = await _classTopicService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get topic by topic id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TopicResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TopicResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _classTopicService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of materials by topic id.
        /// </summary>
        [HttpGet("{id}/materials")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaterialResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<MaterialResponse>>> GetAllMaterialsByClassTopic(Guid id)
        {
            try
            {
                var rs = await _classTopicService.GetAllMaterialsByClassTopic(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new topic.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TopicResponse>> Create([FromBody] TopicRequest request)
        {
            try
            {
                var result = await _classTopicService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete topic by topic id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopicResponse>> Delete(Guid id)
        {
            var rs = await _classTopicService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update topic by topic id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TopicResponse>> Update(Guid id, [FromBody] TopicRequest request)
        {
            try
            {
                var rs = await _classTopicService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get a list of quizzes by topic id.
        /// </summary>
        [HttpGet("{id}/quizzes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuizResponse>>> GetAllQuizzesByClassTopic(Guid id)
        {
            try
            {
                var rs = await _classTopicService.GetAllQuizzesByClassTopic(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get a list of assignments by topic id.
        /// </summary>
        [HttpGet("{id}/assignments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AssignmentResponse>>> GetAllAssignmentsByClassTopic(Guid id)
        {
            try
            {
                var rs = await _classTopicService.GetAllAssignmentsByClassTopic(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
