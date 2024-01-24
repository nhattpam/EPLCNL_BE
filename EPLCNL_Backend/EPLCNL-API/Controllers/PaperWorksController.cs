using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PaperWorksService;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/paper-works")]
    [ApiController]
    public class PaperWorksController : ControllerBase
    {
        private readonly IPaperWorkService _paperWorkService;

        public PaperWorksController(IPaperWorkService paperWorkService)
        {
            _paperWorkService = paperWorkService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaperWorkResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PaperWorkResponse>>> GetAll()
        {
            try
            {
                var rs = await _paperWorkService.GetAll();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaperWorkResponse>> Create([FromBody] PaperWorkRequest request)
        {
            try
            {
                var result = await _paperWorkService.Create(request);
                return CreatedAtAction(nameof(Create), result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<PaperWorkResponse>> Delete([FromQuery] Guid id)
        {
            var rs = await _paperWorkService.Delete(id);
            return Ok(rs);
        }


        [HttpPut]
        public async Task<ActionResult<PaperWorkResponse>> Update([FromQuery] Guid id, [FromBody] PaperWorkRequest request)
        {
            try
            {
                var rs = await _paperWorkService.Update(id, request);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
