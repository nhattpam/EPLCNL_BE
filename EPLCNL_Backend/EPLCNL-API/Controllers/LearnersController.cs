using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.LearnersService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    /// <summary>
    /// Controller for managing learners.
    /// </summary>
    [Route("api/learners")]
    [ApiController]
    public class LearnersController : ControllerBase
    {
        private readonly ILearnerService _learnerService;

        public LearnersController(ILearnerService learnerService)
        {
            _learnerService = learnerService;
        }

        /// <summary>
        /// Get a list of all learners.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LearnerResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<LearnerResponse>>> GetAll()
        {
            try
            {
                var rs = await _learnerService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get learner by learner id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LearnerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LearnerResponse>> Get(Guid id)
        {
            try
            {
                var rs = await _learnerService.Get(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new learner.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LearnerResponse>> Create([FromBody] LearnerRequest request)
        {
            try
            {
                var result = await _learnerService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete learner by learner id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LearnerResponse>> Delete(Guid id)
        {
            var rs = await _learnerService.Delete(id);
            return Ok(rs);
        }

        /// <summary>
        /// Update learner by learner id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<LearnerResponse>> Update(Guid id, [FromBody] LearnerRequest request)
        {
            try
            {
                var rs = await _learnerService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of forums by learner id.
        /// </summary>
        [HttpGet("{id}/forums")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForumResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ForumResponse>>> GetAllForumsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllForumsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Get a list of enrollments by learner id.
        /// </summary>
        [HttpGet("{id}/enrollments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<EnrollmentResponse>>> GetAllEnrollmentsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllEnrollmentsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get list of transactions by learner id.
        /// </summary>
        [HttpGet("{id}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TransactionResponse>> GetAllTransactionsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllTransactionsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get total amount of transactions by learner id.
        /// </summary>
        [HttpGet("{id}/total-amount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<decimal>> GetTotalAmountByLearner(Guid id)
        {
            decimal total = 0;
            try
            {
                var rs = await _learnerService.GetAllEnrollmentsByLearner(id);
                foreach (var enrollment in rs)
                {
                    if(enrollment.RefundStatus==false)
                    {
                        total += (decimal)enrollment.Transaction.Amount;
                    }
                }
                return Ok(total);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get a list of assignment-attempts by learner id.
        /// </summary>
        [HttpGet("{id}/assignment-attempts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssignmentAttemptResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<AssignmentAttemptResponse>>> GetAllAssignmentAttemptsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllAssignmentAttemptsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of quiz-attempts by learner id.
        /// </summary>
        [HttpGet("{id}/quiz-attempts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizAttemptResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuizAttemptResponse>>> GetAllQuizAttemptsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllQuizAttemptsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get a list of refund requests by learner id.
        /// </summary>
        [HttpGet("{id}/refund-requests")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizAttemptResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<RefundResponse>>> GetAllRefundRequestsByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllRefundsByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of profile certificate by learner id.
        /// </summary>
        [HttpGet("{id}/profile-certificates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileCertificateResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ProfileCertificateResponse>>> GetAllProfileCertificatesByLearner(Guid id)
        {
            try
            {
                var rs = await _learnerService.GetAllProfileCertificatesByLearner(id);
                return Ok(rs);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
