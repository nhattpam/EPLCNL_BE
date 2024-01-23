using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AssignmentsService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Get List Product In Menu
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<AssignmentResponse>>> GetAllAssignments()
        {
            try
            {
                var rs = await _assignmentService.GetAssignments();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AssignmentResponse>> Create([FromBody] AssignmentRequest request)
        {
            try
            {
                var result = await _assignmentService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<AssignmentResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _assignmentService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<AssignmentResponse>> Update([FromQuery] Guid id, [FromBody] AssignmentRequest request)
        {
            try
            {
                var rs = await _assignmentService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
